using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace UnityBehaviorTree.Editor
{
    [CreateAssetMenu(fileName = "EditorConfig", menuName = "BehaviorTree/BTEditorConfig", order = 1)]
    public class BTEditorConfig : ScriptableObject
    {
        public VisualTreeAsset GraphViewVirtVisualTreeAsset;
        public StyleSheet GraphViewStyleSheet;
    }
}