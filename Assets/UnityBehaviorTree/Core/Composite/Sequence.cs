using System.Collections.Generic;
using UnityEngine.SubsystemsImplementation;

namespace UnityBehaviorTree.Core.Composite
{
    public class Sequence : CompositeNode
    {
        public Sequence() : base() { }
        
        public Sequence(List<Node> children) : base()
        {
            _children = children;
        }

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override Status OnUpdate()
        {
            foreach (var child in _children)
            {
                child.Tick();
                switch (child._status)
                {
                    case Status.Running:
                        return _status = Status.Running;
                    case Status.Success:
                        continue;
                    case Status.Failed:
                        return _status = Status.Failed;
                    default:
                        return _status = Status.Failed;
                }
            }
            return _status = Status.Success;
        }
    }
}