using System.Collections.Generic;
using UnityEngine.SubsystemsImplementation;

namespace UnityBehaviorTree.Core.Composite
{
    public class Sequence : CompositeNode
    {
        protected int _current = 0;
        
        public Sequence() : base() { }
        
        public Sequence(List<BTNode> children) : base()
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

        protected override EStatus OnUpdate()
        {
            for (int i = _current; i < _children.Count; ++i)
            {
                _current = i;
                _children[i].Tick();
                switch (_children[i].Status)
                {
                    case EStatus.Running:
                        return EStatus.Running;
                    case EStatus.Success:
                        continue;
                    case EStatus.Failed:
                        return EStatus.Failed;
                    default:
                        return EStatus.Failed;
                }
            }

            return EStatus.Success;
        }
        
        public override void ResetData()
        {
            base.ResetData();
            _current = 0;
        }
    }
}