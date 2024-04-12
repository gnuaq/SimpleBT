﻿using System.Collections.Generic;
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

        private void Start()
        {
            _behaviorTree.GenerateTree();
        }

        private void Update()
        {
            _behaviorTree.Tick();
        }
    }
}