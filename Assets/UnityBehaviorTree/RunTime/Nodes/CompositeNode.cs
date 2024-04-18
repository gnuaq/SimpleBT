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

        protected override void InitPort()
        {
            _portConf = new PortConf
            {
                HasInputPort = true,
                InputPortCapacity = PortCapacity.Single,
                HasOutputPort = true,
                OutputPortcapacity = PortCapacity.Multi,
            };
        }
        
        public override void ResetData()
        {
            base.ResetData();
            _children = new List<BTNode>();
        }
    }
}