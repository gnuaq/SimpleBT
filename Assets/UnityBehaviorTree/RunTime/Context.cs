using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace UnityBehaviorTree.Core
{
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