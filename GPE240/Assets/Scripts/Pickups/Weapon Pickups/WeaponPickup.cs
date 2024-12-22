using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Pickup
{
    public Weapon weaponToEquip;
    public float rotateSpeed;

    public override void OnTriggerEnter(Collider other)
    {
        // if we have a weapon to equip (we really ought to because that's the whole purpose of this thing)
        if (weaponToEquip != null)
        {
            // get the pawn that collided with us
            Pawn pawnToEquip = other.GetComponent<Pawn>();
            // if it is indeed a pawn
            if (pawnToEquip != null)
            {
                // tell it to equip our weapon
                pawnToEquip.EquipWeapon(weaponToEquip);
                // no spin :(
                rotateSpeed = 0;
            }
        }
        // do whatever else pickups do on OnTriggerEnter
        base.OnTriggerEnter(other);
    }
    void Update()
    {
        // spin :)
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
}
