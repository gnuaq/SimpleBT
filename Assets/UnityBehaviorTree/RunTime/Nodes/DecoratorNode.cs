using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace UnityBehaviorTree.Core
{
    public abstract class DecoratorNode : BTNode
    {
        private BTNode _child;

        public BTNode Child
        {
            set => _child = value;
            get => _child;
        }
        
        public DecoratorNode() { }

        public DecoratorNode(BTNode child)
        {
            _child = child;
        }
        
        protected override void InitPort()
        {
            _portConf = new PortConf
            {
                HasInputPort = true,
                InputPortCapacity = Port.Capacity.Single,
                HasOutputPort = true,
                OutputPortcapacity = Port.Capacity.Single,
            };
        }
        
        public override BTNode Clone()
        {
            return (BTNode)this.MemberwiseClone();
        }
    }
}