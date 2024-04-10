using UnityEditor.Experimental.GraphView;

namespace UnityBehaviorTree.Core
{
    [System.Serializable]
    public class RootNode : BTNode
    {
        private BTNode _child;
        
        public BTNode Child
        {
            set => _child = value;
            get => _child;
        }

        public RootNode(BTNode child)
        {
            _child = child;
        }
        
        protected override void SetNodeViewData()
        {
            _nodeViewData = new NodeViewData
            {
                HasInputPort = false,
                InputPortCapacity = Port.Capacity.Single,
                HasOutputPort = true,
                OutputPortcapacity = Port.Capacity.Single,
            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override EStatus OnUpdate()
        {
            _child.Tick();
            return _status = _child.Status;
        }
        
        public override BTNode Clone()
        {
            return (BTNode)this.MemberwiseClone();
        }
    }
}