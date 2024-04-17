using UnityBehaviorTree.Core;
using UnityEditor;
using UnityEngine;

namespace UnityBehaviorTree.Editor.Inspector
{
    [CustomEditor(typeof(BehaviorTreeComponent), true)]
    public class BehaviorTreeComponentInspector : UnityEditor.Editor
    {
         public override void OnInspectorGUI()
         {
             base.OnInspectorGUI();

             EditorGUILayout.Space();

             if (GUILayout.Button("Open Editor"))
             {
                 var component = new BehaviorTreeComponent();
                 if (target is BehaviorTreeComponent btcomponent)
                 {
                     component = btcomponent;
                 }
                 BTEditorWindow.ShowBehaviorTreeWindow(component.BehaviorTree);
             }
         }
     }
}