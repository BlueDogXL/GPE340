using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Pickup
{
    public Weapon weaponToEquip;
    public float rotateSpeed;

    public override void OnTriggerEnter(Collider other)
    {
        if (weaponToEquip != null)
        {
            Pawn pawnToEquip = other.GetComponent<Pawn>();
            if (pawnToEquip != null)
            {
                pawnToEquip.EquipWeapon(weaponToEquip);
                rotateSpeed = 0;
            }
        }
        base.OnTriggerEnter(other);
    }
    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
}
