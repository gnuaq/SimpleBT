using System.Collections;
using System.Collections.Generic;
using UnityBehaviorTree.Core;
using UnityEngine;
using UnityEngine.AI;

public class MoveToPos : ActionNode
{
    private NavMeshAgent _navMeshAgent;
    protected override void OnActionStart()
    {
        _navMeshAgent = Context.Agent.GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(Blackboard._targetPos);
        _navMeshAgent.isStopped = false;
    }

    protected override void OnActionStop()
    {
    }

    protected override EStatus OnActionUpdate()
    {
        if (_navMeshAgent.pathPending)
        {
            return EStatus.Running;
        }
        
        if (_navMeshAgent.remainingDistance - _navMeshAgent.stoppingDistance <= 1)
        {
            Blackboard._targetPos = Vector3.zero;
            return EStatus.Success;
        }
        
        if (_navMeshAgent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            Blackboard._targetPos = Vector3.zero;
            return EStatus.Failed;
        }

        return EStatus.Running;
    }
}
