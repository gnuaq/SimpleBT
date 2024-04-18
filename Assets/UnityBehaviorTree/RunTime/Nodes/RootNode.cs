using UnityEngine;

namespace UnityBehaviorTree.Core
{
    public class RootNode : BTNode
    {
        private BTNode _child;
        
        public BTNode Child
        {
            set => _child = value;
            get => _child;
        }
        
        public RootNode() { }

        public RootNode(BTNode child)
        {
            _child = child;
        }
        
        protected override void InitPort()
        {
            _portConf = new PortConf
            {
                HasInputPort = false,
                InputPortCapacity = PortCapacity.Single,
                HasOutputPort = true,
                OutputPortcapacity = PortCapacity.Single,
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
            return _child.Status;
        }
        
        public override void ResetData()
        {
            base.ResetData();
            _child = null;
        }
    }
}