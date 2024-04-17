using UnityEngine;

namespace UnityBehaviorTree.Core
{
    public abstract class ActionNode : BTNode
    {
        private float _initTime;
        private float _currentTime;
        private bool _actionStarted;
        private EStatus _actionStatus;
        // private EStatus _firstTimeActionStatus;
        
        [SerializeField]
        private float _intevalTime = 0.25f;
        
        protected override void InitPort()
        {
            _portConf = new PortConf
            {
                HasInputPort = true,
                InputPortCapacity = PortCapacity.Single,
                HasOutputPort = false,
                OutputPortcapacity = PortCapacity.Single,
            };
        }
        
        protected override void OnStart()
        {
            _initTime = Time.time;
            _currentTime = _initTime;
            _actionStarted = false;
            _actionStatus = EStatus.None;
            // _firstTimeActionStatus = EStatus.None;
        }
        
        protected override EStatus OnUpdate()
        {
            if (!_actionStarted)
            {
                _actionStarted = true;
                OnActionStart();
                // _firstTimeActionStatus = OnActionUpdate();
                _actionStatus = OnActionUpdate();
            }

            _currentTime = Time.time;
            if (_currentTime - _initTime > _intevalTime)
            {
                _initTime += _intevalTime;
                // if (_actionStatus == EStatus.None)
                // {
                //     _actionStatus = _firstTimeActionStatus;
                // }
                
                if (_actionStatus == EStatus.Running)
                {
                    _actionStatus = OnActionUpdate();
                }
            }
            else
            {
                return EStatus.Running;
            }

            if (_actionStatus != EStatus.Running)
            {
                OnActionStop();
                _actionStarted = false;
            }

            return _actionStatus;
        }
        
        protected override void OnStop()
        {
            OnActionStop();
        }

        public override BTNode Clone()
        {
            return (BTNode)this.MemberwiseClone();
        }
        
        public override void ResetData()
        {
            base.ResetData();
            _actionStarted = false;
            _actionStatus = EStatus.None;
        }

        public override void ResetRunningData()
        {
            base.ResetData();
            _actionStarted = false;
            _actionStatus = EStatus.None;
        }
        
        protected abstract void OnActionStart();
        protected abstract EStatus OnActionUpdate();
        protected abstract void OnActionStop();
    }
}