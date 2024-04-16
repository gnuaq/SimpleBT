using UnityBehaviorTree.Core;
using UnityEditor;
using UnityEngine;

namespace UnityBehaviorTree.Editor.Inspector
{
    [CustomEditor(typeof(BTNode), true)]
    public class BehaviorTreeNodeInspector : UnityEditor.Editor
    {
        private readonly GUIContent _descriptionHeader = new GUIContent("Node Description");
        
        private SerializedProperty _nodeTitle;
        private SerializedProperty _nodeComment;
        
        protected virtual void OnEnable()
        {
            _nodeTitle = serializedObject.FindProperty("_nodeViewData.Title");
            _nodeComment = serializedObject.FindProperty("_nodeViewData.Comment");
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawDefaultInspector();
            DrawNodeInspector();
            DrawNodeDescription();
            
            // If the behaviour was edited, update the tree editor and repaint.
            if (GUI.changed)
            {
                serializedObject.ApplyModifiedProperties();
            }
        }

        private void DrawNodeInspector()
        {
            
        }

        private void DrawNodeDescription()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField(_descriptionHeader, EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_nodeTitle);
            EditorGUILayout.PropertyField(_nodeComment);
        }
    }
}