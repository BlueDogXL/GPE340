using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAction_Launcher : WeaponAction
{
    public Transform firePoint;
    private bool isAutoFire;
    public GameObject projectilePrefab;
    private float lastShotTime;

    public override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // if the game is paused don't do anything
        if (GameManager.instance.isPaused) return;
        base.Update();
        // if we're currently autofiring
        if (isAutoFire)
        {
            // shoot
            Shoot();
        }
    }
    public void Shoot()
    {
        // figure out how long we need to wait between each shot
        float secondsPerShot = 1 / weapon.fireRate;
        // if we've waited long enough
        if (Time.time >= lastShotTime + secondsPerShot)
        {
            // "fire in the hole!"
            Debug.Log("Shooting rocket!");
            // make ourselves a rocket at our firePoint transform
            GameObject projectile = Instantiate(projectilePrefab, firePoint);
            // randomly screw with the aim
            projectile.transform.Rotate(0, weapon.GetAccuracyRotationDegrees(), 0);
            // get its data
            Rocket rocketData = projectile.GetComponent<Rocket>();
            // if we have indeed made a projectile with a Rocket component
            if (rocketData != null )
            {
                // tell it to do the amount of damage our weapon does
                rocketData.damage = weapon.damageAmount;
            }
            // update when our last shot was
            lastShotTime = Time.time;
        }
    }
    public void BeginAutoFire()
    {
        // tape down the trigger
        isAutoFire = true;
    }
    public void EndAutoFire()
    {
        // remove the tape from the trigger
        isAutoFire = false;
    }
}
