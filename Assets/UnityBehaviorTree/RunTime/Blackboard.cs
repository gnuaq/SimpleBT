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
            _targetPos = new Vector3();
            _targetPlayer = new GameObject();
        }
    }
}