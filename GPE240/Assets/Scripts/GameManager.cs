using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CameraController mainCamera;
    public PlayerController player;
    public List<AIController> enemies;
    public SpawnPoint[] spawnPoints;
    public bool isPaused = false;

    [Header("Prefabs")]
    public GameObject playerControllerPrefab;
    public GameObject aiControllerPrefab;
    public GameObject playerPawnPrefab;
    public GameObject[] enemyPawnPrefabs;

    [Header("Waves")]
    public int currentWave;
    public int enemiesRemaining;
    public List<WaveData> waves;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        FindCamera();
        FindSpawnPoints();
        SpawnPlayer();
        currentWave = 0;
        SpawnWave(waves[currentWave]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FindCamera()
    {
        // find the camera and store it
        mainCamera = FindObjectOfType<CameraController>();
    }
    private void FindSpawnPoints()
    {
        // find all the spawn point objects
        spawnPoints = FindObjectsOfType<SpawnPoint>();
    }
    public Transform GetRandomSpawnPoint()
    {
        // if we have spawn points
        if (spawnPoints.Length > 0)
        {
            // pick a random one's transform and return it
            return spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
        }
        else
        {
            // return nothing
            return null;
        }
    }
    public void SpawnPlayer()
    {
        player = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity).GetComponent<PlayerController>();
        player.PossessPawn(SpawnPawn());
        mainCamera.target = player.pawn.transform;
        Health playerHealth = player.pawn.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.OnDeath.AddListener(OnPlayerDeath);
        }
    }

    // TODO with extension maybe?: Overload this and SpawnPawn() to match the lectures. for some reason Instantiate<Type>() doesn't work here as it does there and it throws a wrench in the works.
    public void SpawnEnemy()
    {
        // make a new AI controller
        AIController newAI = Instantiate(aiControllerPrefab, Vector3.zero, Quaternion.identity).GetComponent<AIController>();
        // save it to our list
        enemies.Add(newAI);
        // give it a pawn
        newAI.PossessPawn(SpawnPawn());
        Health newAIHealth = newAI.pawn.GetComponent<Health>();
        if (newAIHealth != null)
        {
            newAIHealth.OnDeath.AddListener(OnEnemyDeath);
        }
    }
    
    public Pawn SpawnPawn()
    {
        Transform randomSpawnPoint = GetRandomSpawnPoint();
        Pawn tempPawn = Instantiate(playerPawnPrefab, randomSpawnPoint.transform.position, randomSpawnPoint.transform.rotation).GetComponent<Pawn>();
        return tempPawn;
    }
    public void DoVictory()
    {
        Debug.Log("Thoust hast Vanquishedeth allth thy foes andst emergedeth Victorioust!");
    }
    public void DoGameOver()
    {
        Debug.Log("Thoust Game hath been Overedeth!");
    }
    public void RespawnPlayer()
    {
        if (player.lives > 0)
        {
            Destroy(player.pawn.gameObject);
            player.UnpossessPawn();
            player.PossessPawn(SpawnPawn());
            mainCamera.target = player.pawn.transform;
            Health playerHealth = player.pawn.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.OnDeath.AddListener(OnPlayerDeath);
            }
            player.lives--;
        }
        else
        {
            DoGameOver();
        }
    }
    public void OnPlayerDeath()
    {
        Debug.Log("You Died! Score: 0");
        RespawnPlayer();
    }
    public void Pause()
    {
        Time.timeScale = 0.0f;
        isPaused = true;
    }
    public void Unpause()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
    }
    public void TogglePause()
    {
        if (isPaused)
        {
            Unpause();
        }
        else
        {
            Pause();
        }
    }

    public void SpawnWave(int waveNumber)
    {
        SpawnWave(waves[waveNumber]);
    }
    public void SpawnWave(WaveData wave)
    {
        foreach (Pawn enemyToSpawn in wave.pawns)
        {
            SpawnEnemy(); // TODO if time: change to overload that specifies the pawn once that's figured out
        }
        enemiesRemaining = wave.pawns.Count;
    }
    public void OnEnemyDeath()
    {
        enemiesRemaining--;

        if (enemiesRemaining <= 0)
        {
            currentWave++;
            ClearEnemies();
            if (currentWave < waves.Count)
            {
                SpawnWave(waves[currentWave]);
            }
            else
            {
                DoVictory();
            }
        }
    }
    public void ClearEnemies()
    {
        foreach (AIController enemy in enemies)
        {
            if (enemy != null)
            {
                if (enemy.pawn != null)
                {
                    Destroy(enemy.pawn.gameObject);
                }
                Destroy(enemy.gameObject);
            }
        }
        enemies.Clear();
    }
}
