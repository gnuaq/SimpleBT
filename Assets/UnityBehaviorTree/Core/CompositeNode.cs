using System.Collections.Generic;
using UnityEngine;

namespace UnityBehaviorTree.Core
{
    public abstract class CompositeNode : BTNode
    {
        protected List<BTNode> _children;
        
        public List<BTNode> Children => _children;

        public CompositeNode() : base()
        {
            _children = new List<BTNode>();
        }
        
        public CompositeNode(List<BTNode> children) : base()
        {
            _children = children;
        }
        
        public override BTNode Clone()
        {
            return (BTNode)this.MemberwiseClone();
        }
    }
}