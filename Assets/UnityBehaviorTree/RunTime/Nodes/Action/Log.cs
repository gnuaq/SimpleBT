﻿using UnityEditor;
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
        
        protected override void OnActionStart()
        {
        }

        protected override void OnActionStop()
        {
        }

        protected override EStatus OnActionUpdate()
        {
            Debug.Log($"{Context.Agent.name}: {_msg}");
            return EStatus.Success;
        }
    }
}