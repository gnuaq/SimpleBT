using UnityEditor.Experimental.GraphView;

namespace UnityBehaviorTree.Core
{
    [System.Serializable]
    public abstract class DecoratorNode : BTNode
    {
        private BTNode _child;

        public BTNode Child
        {
            set => _child = value;
            get => _child;
        }

        public DecoratorNode(BTNode child)
        {
            _child = child;
        }
        
        protected override void SetNodeViewData()
        {
            _nodeViewData = new NodeViewData
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