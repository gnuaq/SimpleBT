using UnityEditor;

namespace UnityBehaviorTree.Core
{
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

        public EStatus Status => _status;
        public bool Started => _started;

        public BTNode()
        {
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

        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract EStatus OnUpdate();
        public abstract BTNode Clone();
    }
}