namespace UnityBehaviorTree.Core
{
    public abstract class DecoratorNode : BTNode
    {
        private BTNode _child;

        public BTNode Child
        {
            set => _child = value;
            get => _child;
        }

        public DecoratorNode(BTNode child)
        {
            _child = child;
        }
        
        public override BTNode Clone()
        {
            return (BTNode)this.MemberwiseClone();
        }
    }
}