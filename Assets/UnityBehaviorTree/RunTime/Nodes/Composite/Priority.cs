using System.Collections.Generic;

namespace UnityBehaviorTree.Core.Composite
{
    public class Priority : CompositeNode
    {
        protected int _current = 0;
        
        public Priority() : base() { }
        
        public Priority(List<BTNode> children) : base()
        {
            _children = children;
        }
        
        protected override void OnStart()
        {
            _current = 0;
        }

        protected override void OnStop()
        {
        }

        protected override BTNode.EStatus OnUpdate()
        {
            for (int i = _current; i < _children.Count; ++i)
            {
                _current = i;
                _children[i].Tick();
                switch (_children[i].Status)
                {
                    case BTNode.EStatus.Running:
                        return _status = BTNode.EStatus.Running;
                    case BTNode.EStatus.Success:
                        return _status = BTNode.EStatus.Success;
                    case BTNode.EStatus.Failed:
                        continue;
                    default:
                        return _status = BTNode.EStatus.Failed;
                }
            }

            return _status = BTNode.EStatus.Failed;
        }
    }
}