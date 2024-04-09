using System.Collections.Generic;
using UnityBehaviorTree.Core.Action;
using UnityBehaviorTree.Core.Composite;
using UnityBehaviorTree.Core.Decorator;
using UnityEngine;

namespace UnityBehaviorTree.Core
{
    public class BehaviorTree : MonoBehaviour
    {
        private List<BTNode> _nodes = new List<BTNode>();
        private RootNode _rootNode;

        private void Start()
        {
            // sample tree
            _rootNode = new RootNode(new Repeat(new Sequence(new List<BTNode>
            {
                new Sequence(new List<BTNode>
                {
                    new Log("1"),
                    new Wait(2),
                    new Log("2")
                }),
                new Priority(new List<BTNode>
                {
                    new Log("3"),
                    new Inverter(new Wait(2)),
                    new Log("4")
                }),
                new Wait(2)
            }), 2));
        }

        private void Update()
        {
            if (_rootNode.Status == BTNode.EStatus.Running || _rootNode.Status == BTNode.EStatus.None)
            {
                _rootNode.Tick();
            }
        }
    }
}