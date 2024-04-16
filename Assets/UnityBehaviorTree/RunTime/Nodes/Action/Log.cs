using UnityEditor;
using UnityEngine;

namespace UnityBehaviorTree.Core.Action
{
    public class Log : ActionNode
    {
        [SerializeField]
        private string _msg = "";

        public Log() { }
        
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
            return EStatus.Success;
        }
    }
}