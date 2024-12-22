using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAction_Raygun : WeaponAction
{
    public float fireDistance;
    public Transform firePoint;
    private bool isAutoFire;
    private LineRenderer lineRenderer;
    public LaserBeam laserBeamPrefab;
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
            // "I'MA FIRIN MAH LAZAR"
            Debug.Log("Shooting laser!");

            // get our perfect shot
            Vector3 newFireDirection = firePoint.forward;
            // get a random accuracy change
            Quaternion accuracyFireDelta = Quaternion.Euler(0, weapon.GetAccuracyRotationDegrees(weapon.owner.controller.accuracy), 0);
            // apply it to our direction (has to be in the order quaternion * vector3)
            newFireDirection = accuracyFireDelta * newFireDirection;
            
            // make a ray hit object
            RaycastHit hit;
            // spawn a laser beam
            LaserBeam beam = Instantiate(laserBeamPrefab);
            // tell it where to start
            beam.startPoint = firePoint.position;
            // tell it where to end
            beam.endPoint = newFireDirection.normalized * fireDistance;
            // if we hit something
            if (Physics.Raycast(firePoint.position, newFireDirection, out hit, fireDistance))
            {
                // get its health
                Health otherHealth = hit.collider.gameObject.GetComponent<Health>();
                // if it indeed has a health
                if (otherHealth != null)
                {
                    // zap it
                    otherHealth.TakeDamage(weapon.damageAmount);
                }
            }
            // update our last shot time
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
