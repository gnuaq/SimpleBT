using UnityBehaviorTree.Core;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : ActionNode
{
    private NavMeshAgent _navMeshAgent;
    [SerializeField]
    private float _chaseRange = 10;

    protected override void OnActionStart()
    {
        _navMeshAgent = Context.Agent.GetComponent<NavMeshAgent>();
        _navMeshAgent.isStopped = false;
    }

    protected override void OnActionStop()
    {
    }

    protected override EStatus OnActionUpdate()
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
            Blackboard._targetPos = Vector3.zero;
            return EStatus.Success;
            
        }
        
        _navMeshAgent.SetDestination(Blackboard._targetPos);
        
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            // continues run the tree
            _navMeshAgent.isStopped = true;
            Blackboard._targetPos = Vector3.zero;
            return EStatus.Success;
        }
        
        if (_navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            Blackboard._targetPos = Vector3.zero;
            return EStatus.Failed;
        }
        
        return EStatus.Running;
    }
}
