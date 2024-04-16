using UnityEditor.Experimental.GraphView;

namespace UnityBehaviorTree.Core
{
    public abstract class ActionNode : BTNode
    {
        protected override void InitPort()
        {
            _portConf = new PortConf
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
        
        public override void ResetData()
        {
            base.ResetData();
            _status = EStatus.None;
        }
    }
}