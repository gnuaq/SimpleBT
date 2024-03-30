using UnityEditor;
using UnityEngine;

namespace UnityBehaviorTree.Core.Action
{
    public class Log : ActionNode
    {
        [SerializeField]
        private string _msg = "abc";
        
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override Status OnUpdate()
        {
            Debug.Log($"Log: {_msg}");
            _status = Status.Success;
            return _status;
        }
    }
}