using System.Collections;
using System.Collections.Generic;
using UnityBehaviorTree.Core;
using UnityEngine;

public class CheckPlayerInRange : ActionNode
{
    [SerializeField]
    private float _sightRange = 10;
    
    protected override void OnActionStart()
    {
        // _sight = Context.Agent.GetComponentInChildren<SphereCollider>();
    }

    protected override void OnActionStop()
    {
    }

    protected override EStatus OnActionUpdate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(Context.Agent.transform.position, _sightRange);

        if (hitColliders.Length == 0)
        {
            return EStatus.Failed;
        }
        
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.GetComponent<Player>())
            {
                Blackboard._targetPos = hitCollider.gameObject.transform.position;
                return EStatus.Success;
            }
        }
        return EStatus.Failed;
    }
}
