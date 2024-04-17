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

        private VisualElement _topContainer;
        private VisualElement _bottomContainer;

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

            _topContainer = new VisualElement { name = "TopPortContainer" };
            _bottomContainer = new VisualElement { name = "BottomPortContainer" };
            mainContainer.Insert(0, _topContainer);
            mainContainer.Add(_bottomContainer);
            
            SetNodeViewData(_btNode.NodeViewData);
            HighlightRunning(_btNode.Status);

            RefreshExpandedState();
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
        }

        private void SetTitle(NodeViewData data)
        {
            if (!string.IsNullOrEmpty(data.Title))
                title = data.Title;
        }

        public void AddInputPort(Port.Capacity capacity)
        {
            _inputPort = InstantiatePort(Orientation.Vertical, Direction.Input, capacity, typeof(bool));
            _inputPort.portName = "";
            // _inputPort.RemoveFromClassList("input");
            _topContainer.Add(_inputPort);
        }

        public void AddOutputPort(Port.Capacity capacity)
        {
            _outpurPort = InstantiatePort(Orientation.Vertical, Direction.Output, capacity, typeof(bool));
            _outpurPort.portName = "";
            // _inputPort.RemoveFromClassList("output");
            _bottomContainer.Add(_outpurPort);
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