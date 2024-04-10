namespace UnityBehaviorTree.Core.Decorator
{
    [System.Serializable]
    public class Inverter : DecoratorNode
    {
        public Inverter() { }
        public Inverter(BTNode child) : base(child) { }

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override EStatus OnUpdate()
        {
            Child.Tick();
            switch (Child.Status)
            {
                case EStatus.Success:
                    return _status = EStatus.Failed;
                case EStatus.Failed:
                    return _status = EStatus.Success;
                case EStatus.Running:
                    return _status = EStatus.Running;
                default:
                    return _status = EStatus.Failed;
            }
        }
    }
}