using UnityEditor;
using UnityEngine;

namespace UnityBehaviorTree.Core.Action
{
    [System.Serializable]
    public class Log : ActionNode
    {
        [SerializeField]
        private string _msg = "abc";

        public Log(string Msg)
        {
            _msg = Msg;
        }
        
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override EStatus OnUpdate()
        {
            Debug.Log($"Log: {_msg}");
            _status = EStatus.Success;
            return _status;
        }
    }
}