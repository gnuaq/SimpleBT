using System.Threading;
using UnityEditor;
using UnityEngine;

namespace UnityBehaviorTree.Core.Action
{
    public class Wait : ActionNode
    {
        public Wait() { }
        
        public Wait(float duration)
        {
        }

        protected override void OnActionStart()
        {
        }

        protected override void OnActionStop()
        {
        }

        protected override EStatus OnActionUpdate()
        {
            return EStatus.Success;
        }
    }
}