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
        base.Update();
        if (isAutoFire)
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        float secondsPerShot = 1 / weapon.fireRate;
        if (Time.time >= lastShotTime + secondsPerShot)
        {
            Debug.Log("Shooting laser!");
            // ray hit
            RaycastHit hit;
            LaserBeam beam = LaserBeam.Instantiate(laserBeamPrefab);
            beam.startPoint = firePoint.position;
            beam.endPoint = firePoint.forward.normalized * fireDistance;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, fireDistance))
            {
                Health otherHealth = hit.collider.gameObject.GetComponent<Health>();
                if (otherHealth != null)
                {
                    otherHealth.TakeDamage(weapon.damageAmount);
                }
            }
            lastShotTime = Time.time;
            
        }
    }
    public void BeginAutoFire()
    {
        isAutoFire = true;
    }
    public void EndAutoFire()
    {
        isAutoFire = false;
    }
}
