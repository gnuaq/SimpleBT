using UnityBehaviorTree.Core;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Graphs;

namespace UnityBehaviorTree.Editor
{
    public class BTGraphCallBack
    {
        [MenuItem("Assets/Create/BehaviorTree/CreateBehaviorTreeGraph")]
        public static void CreateBTGraph()
        {
            
        }
        
        [OnOpenAsset]
        public static bool OpenGraphAsset(int instanceID, int line)
        {
            // This gets called whenever ANY asset is double clicked
            // So we gotta check if the asset is of the proper type
            UnityEngine.Object asset = EditorUtility.InstanceIDToObject(instanceID);
            if (!(asset is BehaviorTree)) return false;
 
            bool windowIsOpen = EditorWindow.HasOpenInstances<BTEditorWindow>();
            if (!windowIsOpen) EditorWindow.CreateWindow<BTEditorWindow>();
            else EditorWindow.FocusWindowIfItsOpen<BTEditorWindow>();
            
            BTEditorWindow window = EditorWindow.GetWindow<BTEditorWindow>();
            window.LoadBehaviorTree(instanceID);
            
            return true;
        }
    }
}