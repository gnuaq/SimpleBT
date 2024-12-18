﻿using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityBehaviorTree.Core
{
    [System.Serializable]
    public class Context
    {
        public GameObject Agent;

        public static Context CreateContextFromGameObject(GameObject gameObject)
        {
            Context context = new Context();
            context.Agent = gameObject;
            return context;
        }
    }
}