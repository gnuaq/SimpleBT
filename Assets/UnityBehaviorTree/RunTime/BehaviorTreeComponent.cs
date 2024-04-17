using System.Collections.Generic;
using UnityBehaviorTree.Core.Action;
using UnityBehaviorTree.Core.Composite;
using UnityBehaviorTree.Core.Decorator;
using UnityEditor;
using UnityEngine;

namespace UnityBehaviorTree.Core
{
    public class BehaviorTreeComponent : MonoBehaviour
    {
        [SerializeField]
        private BehaviorTree _behaviorTree;
        
        public BehaviorTree BehaviorTree => _behaviorTree;

        public void Start()
        {
            _behaviorTree = _behaviorTree.CloneTree();
            _behaviorTree.GenerateTree(gameObject);
        }

        public void Update()
        {
            _behaviorTree.Tick();
        }
    }
}