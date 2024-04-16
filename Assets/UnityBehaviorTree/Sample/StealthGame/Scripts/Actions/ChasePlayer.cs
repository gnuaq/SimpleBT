using UnityBehaviorTree.Core;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : ActionNode
{
    private NavMeshAgent _navMeshAgent;
    [SerializeField]
    private float _chaseRange = 10;

    protected override void OnStart()
    {
        _navMeshAgent = Context.Agent.GetComponent<NavMeshAgent>();
    }

    protected override void OnStop()
    {
    }

    protected override EStatus OnUpdate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(Context.Agent.transform.position, _chaseRange);

        int n = 0;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.GetComponent<Player>())
            {
                n++;
                Blackboard._targetPos = hitCollider.gameObject.transform.position;
            }
        }

        if (n == 0)
        {
            // continues run the tree
            _navMeshAgent.isStopped = true;
            return EStatus.Failed;
            
        }
        
        _navMeshAgent.SetDestination(Blackboard._targetPos);
        
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            // continues run the tree
            _navMeshAgent.isStopped = true;
            return EStatus.Failed;
        }
        
        if (_navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            return EStatus.Success;
        }
        
        return EStatus.Running;
    }
}
