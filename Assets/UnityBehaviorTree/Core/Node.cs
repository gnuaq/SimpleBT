using UnityEditor;

namespace UnityBehaviorTree.Core
{
    [System.Serializable]
    public abstract class Node
    {
        public enum Status
        {
            None,
            Running,
            Failed,
            Success,
        }

        public Status _status { protected set; get; }
        public bool _started { private set; get; }

        public Node()
        {
            _status = Status.None;
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

            if (_status != Status.Running)
            {
                OnStop();
                _started = false;
            }
        }

        protected void Attach(Node node)
        {
            
        }

        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract Status OnUpdate();
    }
}