namespace UnityBehaviorTree.Core.Decorator
{
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
                    return EStatus.Failed;
                case EStatus.Failed:
                    return EStatus.Success;
                case EStatus.Running:
                    return EStatus.Running;
                default:
                    return EStatus.Failed;
            }
        }
    }
}