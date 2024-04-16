using UnityBehaviorTree.Core;
using UnityEditor;
using UnityEngine;

namespace UnityBehaviorTree.Editor.Inspector
{
    [CustomEditor(typeof(BTNode), true)]
    public class BehaviorTreeNodeInspector : UnityEditor.Editor
    {
        protected virtual void OnEnable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private void OnPlayModeStateChanged(PlayModeStateChange obj)
        {
            Selection.activeObject = null;
        }

    }
}