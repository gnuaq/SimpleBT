using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace UnityBehaviorTree.Editor.View
{
    public class BTBlackboardField : VisualElement
    {
        private BTBlackboard _blackboard;
        private BlackboardField _blackboardField;
        private BlackboardRow _blackboardRow;
        

        public BTBlackboard BTBlackboard
        {
            set => _blackboard = value;
            get => _blackboard;
        }
        
        public BTBlackboardField()
        {

        }

        public void InitValue(string key, Type type, VisualElement propertyView)
        {
            _blackboardField = new BlackboardField();
            _blackboardField.text = key;
            _blackboardField.typeText = type.ToString();
            _blackboardRow = new BlackboardRow(_blackboardField , propertyView);
            Add(_blackboardRow);
        }
    }
}