using System.Collections.Generic;
using UnityEngine;

namespace UnityBehaviorTree.Core
{
    public abstract class CompositeNode : Node
    {
        protected List<Node> _children;

        public CompositeNode() : base()
        {
            _children = new List<Node>();
        }
        
        public CompositeNode(List<Node> children) : base()
        {
            _children = children;
        }
    }
}