namespace UnityBehaviorTree.Core.Decorator
{
    public class ForceSuccess : DecoratorNode
    {
        public ForceSuccess() { }
        public ForceSuccess(BTNode child) : base(child) { }

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override EStatus OnUpdate()
        {
            Child.Tick();
            if (Child.Status == EStatus.Running)
            {
                return EStatus.Running;
            }
            
            return EStatus.Success;
        }
    }
}