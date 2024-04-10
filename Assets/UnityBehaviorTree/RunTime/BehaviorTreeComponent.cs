using UnityEngine;

namespace UnityBehaviorTree.Core
{
    public class BehaviorTreeComponent : MonoBehaviour
    {
        [SerializeField]
        private BehaviorTree _behaviorTree;

        private void Start()
        {
            
        }

        private void Update()
        {
            _behaviorTree.Tick();
        }
    }
}