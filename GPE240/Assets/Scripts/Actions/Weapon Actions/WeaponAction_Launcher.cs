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
            Debug.Log("Shooting rocket!");
            GameObject projectile = Instantiate(projectilePrefab, firePoint);
            Rocket rocketData = projectile.GetComponent<Rocket>();
            if (rocketData != null )
            {
                rocketData.damage = weapon.damageAmount;
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
