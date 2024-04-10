using System.Collections.Generic;
using UnityBehaviorTree.Core.Action;
using UnityBehaviorTree.Core.Composite;
using UnityBehaviorTree.Core.Decorator;
using UnityEngine;

namespace UnityBehaviorTree.Core
{
    [CreateAssetMenu(fileName = "BehaviorTree", menuName = "BehaviorTree/BehaviorTree")]
    public class BehaviorTree : ScriptableObject
    {
        public List<BTNode> Nodes = new List<BTNode>();
        
        private RootNode _rootNode;

        private void Start()
        {
            // sample tree
            // _rootNode = new RootNode(new Repeat(new Sequence(new List<BTNode>
            // {
            //     new Sequence(new List<BTNode>
            //     {
            //         new Log("1"),
            //         new Wait(2),
            //         new Log("2")
            //     }),
            //     new Priority(new List<BTNode>
            //     {
            //         new Log("3"),
            //         new Inverter(new Wait(2)),
            //         new Log("4")
            //     }),
            //     new Wait(2)
            // }), 2));
        }

        public void Tick()
        {
            if (_rootNode.Status == BTNode.EStatus.Running || _rootNode.Status == BTNode.EStatus.None)
            {
                _rootNode.Tick();
            }
        }
    }
}