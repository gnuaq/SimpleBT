namespace UnityBehaviorTree.Core
{
    public class RootNode : Node
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override Status OnUpdate()
        {
            return Status.Success;
        }
    }
}