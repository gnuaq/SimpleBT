using UnityEditor.Experimental.GraphView;

namespace UnityBehaviorTree.Core
{
    public abstract class ActionNode : BTNode
    {
        protected override void SetNodeViewData()
        {
            _nodeViewData = new NodeViewData
            {
                HasInputPort = true,
                InputPortCapacity = Port.Capacity.Single,
                HasOutputPort = false,
                OutputPortcapacity = Port.Capacity.Single,
            };
        }
        
        public override BTNode Clone()
        {
            return (BTNode)this.MemberwiseClone();
        }
    }
}