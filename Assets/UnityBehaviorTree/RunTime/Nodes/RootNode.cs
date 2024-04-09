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

        public RootNode(BTNode child)
        {
            _child = child;
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