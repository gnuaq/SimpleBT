using UnityEditor;
using UnityEditor.Experimental.GraphView;

namespace UnityBehaviorTree.Core
{
    public class NodeViewData
    {
        public string Title;
        public bool HasInputPort;
        public Port.Capacity InputPortCapacity;
        public bool HasOutputPort;
        public Port.Capacity OutputPortcapacity;
    }
    
    [System.Serializable]
    public abstract class BTNode
    {
        public enum EStatus
        {
            None,
            Running,
            Failed,
            Success,
        }

        private bool _started;

        protected EStatus _status;
        protected NodeViewData _nodeViewData;

        public EStatus Status => _status;
        public bool Started => _started;
        public NodeViewData NodeViewData;

        public BTNode()
        {
            SetNodeViewData();
            _status = EStatus.None;
            _started = false;
        }

        public void Tick()
        {
            if (!_started)
            {
                OnStart();
                _started = true;
            }

            OnUpdate();

            if (_status != EStatus.Running)
            {
                OnStop();
                _started = false;
            }
        }

        protected void Attach(BTNode node)
        {
            
        }

        protected abstract void SetNodeViewData();
        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract EStatus OnUpdate();
        public abstract BTNode Clone();
    }
}