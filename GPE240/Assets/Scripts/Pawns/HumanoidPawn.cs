using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidPawn : Pawn
{
    private Animator animator;
    public Transform weaponAttachmentPoint;
    public ParticleSystem particles;

    // Start is called before the first frame update
    public override void Start()
    {
        // get our animator
        animator = GetComponent<Animator>();
        base.Start();
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
        // rotate our transform
        transform.Rotate(0, speed * maxRotationSpeed * Time.deltaTime, 0);
    }
    public override void RotateToLookAt(Vector3 targetPoint)
    {
        // find where to look
        Vector3 lookVector = targetPoint - transform.position;
        // find how to look where we need to look
        Quaternion lookRotation = Quaternion.LookRotation(lookVector, Vector3.up);
        // rotate towards where we need to look
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, maxRotationSpeed * Time.deltaTime);
    }
    public override void EquipWeapon(Weapon weaponToEquip)
    {
        // we want the new weapon over the old one so toss that hunk of junk
        UnequipWeapon();
        // set our new weapon as whatever weapon we're picking up
        weapon = Instantiate(weaponToEquip, weaponAttachmentPoint) as Weapon;
        // set it to our layer so we don't have to worry about hitting ourselves
        weapon.gameObject.layer = this.gameObject.layer;
        // tell the weapon we own it
        weapon.owner = this;
    }
    public override void UnequipWeapon()
    {
        // if we have a weapon to unequip
        if (weapon != null)
        {
            // forsake it
            weapon.owner = null;
            // destroy it
            Destroy(weapon.gameObject);
        }
        // we have no weapon now
        weapon = null;
    }
    public void OnAnimatorIK()
    {
        // if we have an animator
        if (animator != null)
        {
            // if we don't have a weapon
            if (weapon == null)
            {
                // we don't have goals so just let the animation do its thing
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
                // we're done here
                return;
            }
            // if we have a right hand target
            if (weapon.RightHandIKTarget)
            {
                // tell its position to the animator
                animator.SetIKPosition(AvatarIKGoal.RightHand, weapon.RightHandIKTarget.position);
                // set the weight to 1 (as much as possible, match the goal position)
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
                // ditto, but with rotation
                animator.SetIKRotation(AvatarIKGoal.RightHand, weapon.RightHandIKTarget.rotation);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
            }
            else
            {
                // we're chill about it, let the animation do its thing
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);
            }
            // if we have a left hand target
            if (weapon.LeftHandIKTarget)
            {
                // tell its position to the animator
                animator.SetIKPosition(AvatarIKGoal.LeftHand, weapon.LeftHandIKTarget.position);
                // set the weight to 1 (as much as possible, match the goal position)
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
                // ditto, but with rotation
                animator.SetIKRotation(AvatarIKGoal.LeftHand, weapon.LeftHandIKTarget.rotation);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
            }
            else
            {
                // we're chill about it, let the animation do its thing
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
            }
        }
    }
    public void OnAnimatorMove()
    {
        // set our position to the root position
        transform.position = animator.rootPosition;
        // set our rotation to the root rotation
        transform.rotation = animator.rootRotation;
        // check if we're an ai
        AIController aiController = controller as AIController;
        // if we are indeed an ai
        if (aiController != null)
        {
            // set our next position to the root position
            aiController.agent.nextPosition = animator.rootPosition;
        }
    }
}
