using System.Collections;
using System.Collections.Generic;
using UnityBehaviorTree.Core;
using UnityEngine;

public class GuardAI : MonoBehaviour
{
    [SerializeField]
    private BehaviorTree _behaviorTree;

    private void Start()
    {
        _behaviorTree.GenerateTree(gameObject);
    }

    private void Update()
    {
        _behaviorTree.Tick();
    }
}
