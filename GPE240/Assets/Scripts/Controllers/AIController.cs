using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : Controller
{
    public NavMeshAgent agent;
    public float stoppingDistance;
    public float shootingDistance;
    public float shootingAngle;
    public Transform targetTransform;
    private Vector3 desiredVelocity = Vector3.zero;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        // if we don't have a target
        if (!HasTarget())
        {
            Debug.Log("Targeting player!");
            // target the player
            TargetPlayer();
        }
        // do controller stuff
        base.Update(); 
    }

    public override void PossessPawn(Pawn pawnToPossess)
    {
        // do base controller stuff
        base.PossessPawn(pawnToPossess);
        // if the pawn has a navmeshagent already, use that
        agent = pawn.GetComponent<NavMeshAgent>();
        // if not
        if (agent == null )
        {
            // make one and add it to the pawn
            agent = pawn.gameObject.AddComponent<NavMeshAgent>();
        }
        // set our variables onto the agent so it can do its agent stuff accurately
        agent.stoppingDistance = stoppingDistance;
        agent.speed = pawn.maxMoveSpeed;
        agent.angularSpeed = pawn.maxRotationSpeed;
        agent.updatePosition = false;
        agent.updateRotation = false;
    }
    public override void UnpossessPawn()
    {
        // get rid of our navmeshagent
        Destroy(agent);
        // do base controller stuff
        base.UnpossessPawn();
    }
    protected override void MakeDecisions()
    {
        // if we have no pawn, we do not need to be making decisions
        if (pawn == null)
        {
            return;
        }
        // if we have a weapon
        if (pawn.weapon != null)
        {
            // if we have a target
            if (targetTransform != null)
            {
                // put our target's transform into our gps i mean navmeshagent
                agent.SetDestination(targetTransform.position);
                // get our direction and speed from the agent
                desiredVelocity = agent.desiredVelocity;
                // normalize it and send it to the pawn to do the actual moving
                pawn.Move(desiredVelocity.normalized);
                // look at our target so it knows we're coming for it
                pawn.RotateToLookAt(targetTransform.position);

                // if we're in shooting range
                if (Vector3.Distance(targetTransform.position, pawn.transform.position) <= shootingDistance)
                {
                    Debug.Log(pawn.gameObject.name + " is in range!");
                    // and we're looking the right way
                    Vector3 vectorToTarget = targetTransform.position - pawn.transform.position;
                    if (Vector3.Angle(pawn.transform.forward, vectorToTarget) <= shootingAngle)
                    {
                        Debug.Log(pawn.gameObject.name + "is in angle!");
                        // shoot your shot
                        pawn.weapon.OnPrimaryAttackBegin.Invoke();
                    }
                    else
                    {
                        // there's nothing there, you can stop shooting now
                        pawn.weapon.OnPrimaryAttackEnd.Invoke();
                    }
                }
            }
        }
    }
    private bool HasTarget()
    {
        // if we have a target
        if (targetTransform != null)
        {
            // say that we do
            return true;
        }
        // if not
        else
        {
            // say that we don't
            return false;
        }
    }
    private void TargetPlayer()
    {
        // find the player controller
        Controller playerController = FindObjectOfType<PlayerController>();
        // if we found one
        if (playerController != null)
        {
            // if it has a pawn
            if (playerController.pawn != null)
            {
                // target acquired
                targetTransform = playerController.pawn.transform;
            }
        }
    }
}
