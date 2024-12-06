using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    public Controller controller;
    public float maxMoveSpeed;
    public float maxRotationSpeed;
    public Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Move(Vector3 direction);
    public abstract void Rotate(float speed);
    public abstract void RotateToLookAt(Vector3 targetPoint);
    public abstract void EquipWeapon(Weapon weaponToEquip);
    public abstract void UnequipWeapon();
}
