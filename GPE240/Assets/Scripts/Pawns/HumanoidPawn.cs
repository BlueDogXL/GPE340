using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidPawn : Pawn
{
    private Animator animator;
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
        // send direction to the animator
        animator.SetFloat("Forward", direction.z);
        animator.SetFloat("Right", direction.x);
    }
}
