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
        private Port _inputPort;
        private Port _outpurPort;
        
        public BTNode BTNode
        {
            set { _btNode = value; }
            get { return _btNode; }
        }

        public Port InputPort => _inputPort;
        public Port OutputPort => _outpurPort;
        
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

            viewDataKey = _btNode.UID;
            
            var port = _btNode.PortConf;
            var nodeViewData = _btNode.NodeViewData;

            if (!string.IsNullOrEmpty(nodeViewData.Title))
                title = nodeViewData.Title;
            if (port.HasInputPort)
                AddInputPort(port.InputPortCapacity);
            if (port.HasOutputPort)
                AddOutputPort(port.OutputPortcapacity);
        }

        public void AddInputPort(Port.Capacity capacity)
        {
            _inputPort = InstantiatePort(Orientation.Vertical, Direction.Input, capacity, typeof(bool));
            _inputPort.portName = "";
            inputContainer.Add(_inputPort);
        }

        public void AddOutputPort(Port.Capacity capacity)
        {
            _outpurPort = InstantiatePort(Orientation.Vertical, Direction.Output, capacity, typeof(bool));
            _outpurPort.portName = "";
            outputContainer.Add(_outpurPort);
        }
    }
}