using System.Collections;
using System.Collections.Generic;
using UnityBehaviorTree.Core;
using UnityEngine;
using UnityEngine.AI;

public class MoveToPos : ActionNode
{
    private NavMeshAgent _navMeshAgent;
    protected override void OnStart()
    {
        _navMeshAgent = Context.Agent.GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(Blackboard._targetPos);
    }

    protected override void OnStop()
    {
    }

    protected override EStatus OnUpdate()
    {
        if (_navMeshAgent.pathPending)
        {
            return EStatus.Running;
        }
        
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            return EStatus.Success;
        }
        
        if (_navMeshAgent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            return EStatus.Failed;
        }

        return EStatus.Running;
    }
}
