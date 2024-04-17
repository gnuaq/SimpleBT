using System.Collections;
using System.Collections.Generic;
using UnityBehaviorTree.Core;
using UnityEngine;
using UnityEngine.AI;

public class GetRandomPos : ActionNode
{
    [SerializeField]
    private float _navigateRadius = 5;

    protected override void OnActionStart()
    {
    }

    protected override void OnActionStop()
    {
    }

    protected override EStatus OnActionUpdate()
    {
        if (Blackboard._targetPos != Vector3.zero)
        {
            return EStatus.Failed;
        }
        Vector3 randomDirection = Random.insideUnitSphere * _navigateRadius;
        randomDirection += Context.Agent.transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, _navigateRadius, 1))
        {
            finalPosition = hit.position;
            Debug.Log($"final pos {finalPosition}");
            Blackboard._targetPos = finalPosition;
            return EStatus.Success;
        }
        else
        {
            return EStatus.Failed;
        }
        
    }
}
