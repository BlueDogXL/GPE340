using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : Controller
{
    public NavMeshAgent agent;
    public float stoppingDistance;
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
        base.Update(); 
    }

    public override void PossessPawn(Pawn pawnToPossess)
    {
        base.PossessPawn(pawnToPossess);
        agent = pawn.GetComponent<NavMeshAgent>();
        if (agent == null )
        {
            agent = pawn.gameObject.AddComponent<NavMeshAgent>();
        }
        agent.stoppingDistance = stoppingDistance;
        agent.speed = pawn.maxMoveSpeed;
        agent.angularSpeed = pawn.maxRotationSpeed;
        agent.updatePosition = false;
        agent.updateRotation = false;
    }
    public override void UnpossessPawn()
    {
        Destroy(agent);
        base.UnpossessPawn();
    }
    protected override void MakeDecisions()
    {
        if (pawn == null)
        {
            return;
        }
        agent.SetDestination(targetTransform.position);
        desiredVelocity = agent.desiredVelocity;
        pawn.Move(desiredVelocity.normalized);
        pawn.RotateToLookAt(targetTransform.position);
    }
}
