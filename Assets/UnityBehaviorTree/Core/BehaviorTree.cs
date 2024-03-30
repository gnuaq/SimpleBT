using System.Collections.Generic;
using UnityBehaviorTree.Core.Action;
using UnityBehaviorTree.Core.Composite;
using UnityEngine;

namespace UnityBehaviorTree.Core
{
    public class BehaviorTree : MonoBehaviour
    {
        private List<Node> _nodes = new List<Node>();

        private void Start()
        {
            // _nodes.Add(new Log());
            // _nodes.Add(new Wait());
            _nodes.Add(new Sequence(new List<Node>{new Wait(), new Log()}));
        }

        private void Update()
        {
            foreach (var node in _nodes)
            {
                node.Tick();
            }
        }
    }
}