using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace UnityBehaviorTree.Core
{
    [System.Serializable]
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

        protected override void SetNodeViewData()
        {
            _nodeViewData = new NodeViewData
            {
                HasInputPort = true,
                InputPortCapacity = Port.Capacity.Single,
                HasOutputPort = true,
                OutputPortcapacity = Port.Capacity.Multi,
            };
        }
        
        public override BTNode Clone()
        {
            return (BTNode)this.MemberwiseClone();
        }
    }
}