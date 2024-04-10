using System;
using System.Collections.Generic;
using System.Linq;
using UnityBehaviorTree.Core;
using UnityBehaviorTree.Core.Action;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityBehaviorTree.Editor.View
{
    public class BTGraphView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<BTGraphView, GraphView.UxmlTraits> { }

        private BehaviorTree _behaviorTree;
        private BTEditorWindow _btEditorWindow;
        private NodeSearchWindow _nodeSearchWindow;

        public BTEditorWindow BTEditorWindow
        {
            set => _btEditorWindow = value;
            get => _btEditorWindow;
        }
        
        public BehaviorTree BehaviorTree
        {
            set => _behaviorTree = value;
            get => _behaviorTree;
        }
        
        public BTGraphView()
        {
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            GridBackground gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();
            Insert(0, gridBackground);

            _nodeSearchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
            _nodeSearchWindow.BTGraphView = this;
            _nodeSearchWindow.BTEditorWindow = _btEditorWindow;
            nodeCreationRequest += context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _nodeSearchWindow);

            graphViewChanged += OnGraphViewChanged;
        }
        
        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if (graphViewChange.elementsToRemove != null)
            {
                
            }
            return graphViewChange;
        }


        public void AddNode(Type type)
        {
            BTNode node = Activator.CreateInstance(type) as BTNode;
            node.NodeViewData.Title = type.Name;
            _behaviorTree.Nodes.Add(node);
            AddNodeView(node);
        }

        public void AddNode(BTNode node)
        {
            _behaviorTree.Nodes.Add(node);
            AddNodeView(node);
        }

        public void AddNodeView(BTNode node)
        {
            BTNodeView btNodeView = new BTNodeView(node);
            AddElement(btNodeView);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList().Where(endPort =>
                endPort.direction != startPort.direction &&
                endPort.node != startPort.node).ToList();
        }
    }
}