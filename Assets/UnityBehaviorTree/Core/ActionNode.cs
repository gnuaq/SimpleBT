namespace UnityBehaviorTree.Core
{
    public abstract class ActionNode : BTNode
    {
        public override BTNode Clone()
        {
            return (BTNode)this.MemberwiseClone();
        }
    }
}