using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityBehaviorTree.Editor.View
{
    public class BTBlackboard : Blackboard
    {
        public string _title = "Black board";
        public BTBlackboard()
        {
            base.title = _title;
            addItemRequested += AddItemRequest;
        }

        private void AddItemRequest(Blackboard blackboard)
        {
            // var dropdown = new DropdownField("Options", new List<string> { "Option 1", "Option 2", "Option 3" }, 0);
            // // Add another option
            // dropdown.choices.Add("Option 4");
            // // To return int value instead of string
            // dropdown.index = 1; // Option 2
            // dropdown.value = "Option3";
            // // Register to the value changed callback
            // dropdown.RegisterValueChangedCallback(evt => Debug.Log(evt.newValue));
            // Add(dropdown);
            
            
            
            // var bbField = new BTBlackboardField();
            // bbField.InitValue("test abc", typeof(string), new Label("abc"));
            // Add(bbField);
        }
    }
}