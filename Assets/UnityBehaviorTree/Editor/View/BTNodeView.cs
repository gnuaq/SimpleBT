using System;
using UnityBehaviorTree.Core;
using UnityBehaviorTree.Core.Action;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace UnityBehaviorTree.Editor.View
{
    public class BTNodeView : Node
    {
        private BTNode _btNode;
        
        public BTNode BTNode
        {
            set { _btNode = value; }
            get { return _btNode; }
        }
        
        public BTNodeView(BTNode node)
        {
            _btNode = node;
            
            SetNodeViewData();
        }

        private void SetNodeViewData()
        {
            if (_btNode.NodeViewData is null)
            {
                Debug.Log("NodeViewData is Null");
                return;
            }
                
            var nodeViewData = _btNode.NodeViewData;
            if (!string.IsNullOrEmpty(nodeViewData.Title))
                title = nodeViewData.Title;
            if (nodeViewData.HasInputPort)
                AddInputPort(nodeViewData.InputPortCapacity);
            if (nodeViewData.HasOutputPort)
                AddOutputPort(nodeViewData.OutputPortcapacity);
        }

        public void AddInputPort(Port.Capacity capacity)
        {
            var inputPort = InstantiatePort(Orientation.Vertical, Direction.Input, capacity, typeof(bool));
            inputPort.portName = "";
            inputContainer.Add(inputPort);
        }

        public void AddOutputPort(Port.Capacity capacity)
        {
            var outputPort = InstantiatePort(Orientation.Vertical, Direction.Output, capacity, typeof(bool));
            outputPort.portName = "";
            inputContainer.Add(outputPort);
        }
    }
}