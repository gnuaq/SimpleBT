using UnityEditor;

namespace UnityBehaviorTree.Editor
{
    public class BTGraphCallBack
    {
        [MenuItem("Assets/Create/BehaviorTree/CreateBehaviorTreeGraph")]
        public static void CreateBTGraph()
        {
            
        }

        [MenuItem("BehaviorTree/OpenBehaviorTreeWindow")]
        public static void OpenBTWindow()
        {
            BTEditorWindow btEditorWindow = new BTEditorWindow();
            btEditorWindow.Show();
        }
    }
}