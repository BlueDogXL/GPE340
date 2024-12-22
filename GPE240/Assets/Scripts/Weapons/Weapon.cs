using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    public float damageAmount;
    public float fireRate;
    public float maxAccuracyRotation;
    [Header("IK Targets")]
    public Transform RightHandIKTarget;
    public Transform LeftHandIKTarget;
    [Header("Events")]
    public UnityEvent OnPrimaryAttackBegin;
    public UnityEvent OnPrimaryAttackEnd;
    public UnityEvent OnSecondaryAttackBegin;
    public UnityEvent OnSecondaryAttackEnd;
    [HideInInspector]
    public Pawn owner;
    
    public virtual float GetAccuracyRotationDegrees(float accuracyMod = 1)
    {
        // get a random percent
        float accuracyDeltaPercent = Random.value;
        // find that percent between our min and max range
        float accuracyDeltaDegrees = Mathf.Lerp(-maxAccuracyRotation, maxAccuracyRotation, accuracyDeltaPercent);
        // multiply it by accuracy 
        accuracyDeltaDegrees *= accuracyMod;
        // send it
        return accuracyDeltaDegrees;
    }
}
