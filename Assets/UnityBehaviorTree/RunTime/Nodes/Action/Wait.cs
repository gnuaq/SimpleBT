using System.Threading;
using UnityEditor;
using UnityEngine;

namespace UnityBehaviorTree.Core.Action
{
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

        protected override void OnActionStart()
        {
            _time = 0;
        }

        protected override void OnActionStop()
        {
        }

        protected override EStatus OnActionUpdate()
        {
            EStatus status;
            if (_time < _duration)
            {
                _time += Time.deltaTime;
                status = EStatus.Running;
            }
            else
            {
                status =  EStatus.Success;
            }

            return status;
        }
    }
}