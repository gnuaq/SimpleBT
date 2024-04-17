using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityBehaviorTree.Core
{
    [Serializable]
    public class Blackboard
    {
        public Vector3 _targetPos;
        public GameObject _targetPlayer;

        public Blackboard()
        {
        }

        public void ResetData()
        {
            _targetPos = Vector3.zero;
            _targetPlayer = null;
        }
    }
}