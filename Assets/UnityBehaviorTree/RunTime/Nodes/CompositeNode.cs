using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
                InputPortCapacity = Port.Capacity.Single,
                HasOutputPort = true,
                OutputPortcapacity = Port.Capacity.Multi,
            };
        }
        
        public override BTNode Clone()
        {
            return (BTNode)this.MemberwiseClone();
        }
        
        public override void ResetData()
        {
            base.ResetData();
            _status = EStatus.None;
            _children = new List<BTNode>();
        }
    }
}