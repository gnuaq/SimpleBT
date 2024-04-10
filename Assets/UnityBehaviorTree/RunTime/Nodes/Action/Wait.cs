using System.Threading;
using UnityEditor;
using UnityEngine;

namespace UnityBehaviorTree.Core.Action
{
    [System.Serializable]
    public class Wait : ActionNode
    {
        [SerializeField]
        private float _duration = 1;

        private float _time;

        public Wait() { }
        
        public Wait(float duration)
        {
            _duration = duration;
        }

        protected override void OnStart()
        {
            _time = 0;
        }

        protected override void OnStop()
        {
        }

        protected override EStatus OnUpdate()
        {
            if (_time < _duration)
            {
                _time += Time.deltaTime;
                _status = EStatus.Running;
            }
            else
            {
                _status =  EStatus.Success;
            }

            return _status;
        }
    }
}