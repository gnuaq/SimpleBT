using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityBehaviorTree.Core
{
    [Serializable]
    public class Blackboard
    {
        public Vector3 vec3Test;

        public Blackboard()
        {
            vec3Test = new Vector3(1, 0, 1);
        }
    }
}