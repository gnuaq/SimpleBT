using System;
using UnityBehaviorTree.Core;
using UnityBehaviorTree.Core.Action;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

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
            _btNode.OnNodeDataChange += OnNodeDataChange;
            _btNode.OnStatusChange += HighlightRunning;

            // change input, output container position
            inputContainer.RemoveFromHierarchy();
            outputContainer.RemoveFromHierarchy();
            mainContainer.Insert(0, inputContainer);
            mainContainer.Add(outputContainer);
            inputContainer.AddToClassList("PortContainer");
            outputContainer.AddToClassList("PortContainer");

            SetNodeViewData(_btNode.NodeViewData);
            HighlightRunning(_btNode.Status);
        }

        private void OnNodeDataChange(NodeViewData nodeViewData)
        {
            SetTitle(nodeViewData);
        }

        public override void OnSelected()
        {
            Selection.activeObject = _btNode;
            base.OnSelected();
        }

        private void SetNodeViewData(NodeViewData data)
        {
            if (data is null)
            {
                Debug.Log("NodeViewData is Null");
                return;
            }

            viewDataKey = _btNode.UID;
            
            var port = _btNode.PortConf;

            SetTitle(data);
            
            if (port.HasInputPort)
                AddInputPort(port.InputPortCapacity);
            if (port.HasOutputPort)
                AddOutputPort(port.OutputPortcapacity);

            // type
            titleContainer.Add(new Label(string.Format("<< {0} >>", _btNode.GetNodeType())));
        }

        private void SetTitle(NodeViewData data)
        {
            if (!string.IsNullOrEmpty(data.Title))
                title = data.Title;
        }

        public void AddInputPort(PortCapacity capacity)
        {
            _inputPort = InstantiatePort(Orientation.Vertical, Direction.Input, ConvertPort(capacity), typeof(bool));
            _inputPort.portName = "";
            // _inputPort.RemoveFromClassList("input");
            inputContainer.Add(_inputPort);
        }

        public void AddOutputPort(PortCapacity capacity)
        {
            _outpurPort = InstantiatePort(Orientation.Vertical, Direction.Output, ConvertPort(capacity), typeof(bool));
            _outpurPort.portName = "";
            // _inputPort.RemoveFromClassList("output");
            outputContainer.Add(_outpurPort);
        }

        public Port.Capacity ConvertPort(PortCapacity dataPortCapacity)
        {
            Port.Capacity capacity = Port.Capacity.Single;
            
            switch (dataPortCapacity)
            {
                case PortCapacity.Multi:
                    capacity = Port.Capacity.Multi;
                    break;
                case PortCapacity.Single:
                    capacity = Port.Capacity.Single;
                    break;
                default:
                    Debug.LogWarning("cannot find capacity type. Use Port.Capacity.Single");
                    capacity = Port.Capacity.Single;
                    break;
            }

            return capacity;
        }

        public void HighlightRunning(BTNode.EStatus status)
        {
            if (status == BTNode.EStatus.Running)
            {
                mainContainer.AddToClassList("running");
            }
            else
            {
                mainContainer.RemoveFromClassList("running");
            }
        }
    }
}