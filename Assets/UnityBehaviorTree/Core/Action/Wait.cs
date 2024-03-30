using System.Threading;
using UnityEditor;
using UnityEngine;

namespace UnityBehaviorTree.Core.Action
{
    public class Wait : ActionNode
    {
        [SerializeField]
        private float _duration = 3;

        private float _time;

        protected override void OnStart()
        {
            _time = 0;
        }

        protected override void OnStop()
        {
        }

        protected override Status OnUpdate()
        {
            if (_time < _duration)
            {
                _time += Time.deltaTime;
                _status = Status.Running;
            }
            else
            {
                _status =  Status.Success;
            }

            return _status;
        }
    }
}