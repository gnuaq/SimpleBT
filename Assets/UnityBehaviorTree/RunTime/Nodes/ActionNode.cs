using UnityEditor.Experimental.GraphView;

namespace UnityBehaviorTree.Core
{
    [System.Serializable]
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
    }
}