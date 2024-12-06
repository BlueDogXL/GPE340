using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidPawn : Pawn
{
    private Animator animator;
    public Transform weaponAttachmentPoint;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void Move(Vector3 direction)
    {
        // convert direction from world space to local space for the animator
        direction = transform.InverseTransformDirection(direction);
        // send direction to the animator
        animator.SetFloat("Forward", direction.z);
        animator.SetFloat("Right", direction.x);
    }
    public override void Rotate(float speed)
    {
        transform.Rotate(0, speed * maxRotationSpeed * Time.deltaTime, 0);
    }
    public override void RotateToLookAt(Vector3 targetPoint)
    {
        Vector3 lookVector = targetPoint - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(lookVector, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, maxRotationSpeed * Time.deltaTime);
    }
    public override void EquipWeapon(Weapon weaponToEquip)
    {
        UnequipWeapon();
        weapon = Instantiate(weaponToEquip, weaponAttachmentPoint) as Weapon;
    }
    public override void UnequipWeapon()
    {
        if (weapon != null)
        {
            Destroy(weapon.gameObject);
        }
        weapon = null;
    }
    public void OnAnimatorIK()
    {
        if (animator != null)
        {
            if (weapon == null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
                return;
            }
            if (weapon.RightHandIKTarget)
            {
                animator.SetIKPosition(AvatarIKGoal.RightHand, weapon.RightHandIKTarget.position);
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
                animator.SetIKRotation(AvatarIKGoal.RightHand, weapon.RightHandIKTarget.rotation);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
            }
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);
            }
            if (weapon.LeftHandIKTarget)
            {
                animator.SetIKPosition(AvatarIKGoal.LeftHand, weapon.LeftHandIKTarget.position);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, weapon.LeftHandIKTarget.rotation);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
            }
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
            }
        }
    }
    public void OnAnimatorMove()
    {
        transform.position = animator.rootPosition;
        transform.rotation = animator.rootRotation;
        AIController aiController = controller as AIController;
        if (aiController != null)
        {
            aiController.agent.nextPosition = animator.rootPosition;
        }
    }
}
