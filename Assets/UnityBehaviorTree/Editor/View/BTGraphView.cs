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
            nodeCreationRequest += context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _nodeSearchWindow);

            graphViewChanged += OnGraphViewChanged;
        }

        public void SetEditorWindow(BTEditorWindow editorWindow)
        {
            _btEditorWindow = editorWindow;
            _nodeSearchWindow.BTEditorWindow = editorWindow;
        }
        
        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if (graphViewChange.elementsToRemove != null)
            {
                foreach (var graphElement in graphViewChange.elementsToRemove)
                {
                    if (graphElement is Node node)
                    {
                        var nodeView = node as BTNodeView;
                        _behaviorTree.RemoveNode(nodeView.BTNode.UID);
                    }
                
                    if (graphElement is Edge edge)
                    {
                        if (edge.input != null && edge.output != null)
                        {
                            var inputNodeView = edge.input.node as BTNodeView;
                            var outputNodeView = edge.output.node as BTNodeView;
                            _behaviorTree.RemoveEdge(inputNodeView.BTNode, outputNodeView.BTNode);
                        }
                    }
                }
            }

            if (graphViewChange.edgesToCreate != null)
            {
                var edges = graphViewChange.edgesToCreate;
                foreach (var edge in edges)
                {
                    if (edge.input != null && edge.output != null)
                    {
                        var inputNodeView = edge.input.node as BTNodeView;
                        var outputNodeView = edge.output.node as BTNodeView;
                        _behaviorTree.AddEdge(inputNodeView.BTNode, outputNodeView.BTNode);
                    }
                }
            }

            if (graphViewChange.movedElements != null)
            {
                foreach (var element in graphViewChange.movedElements)
                { 
                    if (element is Node node)
                    {
                        var nodeView = node as BTNodeView;
                        var btNode = _behaviorTree.FindNodeByID(nodeView.BTNode.UID);
                        _behaviorTree.MoveNode(btNode, nodeView.GetPosition().position);
                    }
                }
            }

            return graphViewChange;
        }

        public void LoadGraph()
        {
            foreach (var node in _behaviorTree.Nodes)
            {
                var nodeView = AddNodeView(node, node.NodeViewData.Position);
            }
            
            foreach (var node in _behaviorTree.Nodes)
            {
                var parentNodeView = GetNodeByGuid(node.UID) as BTNodeView;
                foreach (var outputNodeID in node.NodeViewData.OutputNodeIDs)
                {
                    var childNodeView = GetNodeByGuid(outputNodeID) as BTNodeView;
                    var edge = parentNodeView.OutputPort.ConnectTo(childNodeView.InputPort);
                    AddElement(edge);
                }
            }
        }

        public void AddNode(Type type, Vector2 pos)
        {
            var node = _behaviorTree.AddNode(type, pos);
            AddNodeView(node, pos);
        }
        
        public void AddRootNote(Vector2 pos)
        {
            var node = _behaviorTree.AddRootNode(pos);
            AddNodeView(node, pos);
        }

        public BTNodeView AddNodeView(BTNode node)
        {
            BTNodeView btNodeView = new BTNodeView(node);
            AddElement(btNodeView);
            return btNodeView;
        }
        
        public BTNodeView AddNodeView(BTNode node, Vector2 pos)
        {
            BTNodeView btNodeView = new BTNodeView(node);
            btNodeView.SetPosition(new Rect(pos, Vector2.zero));
            AddElement(btNodeView);
            return btNodeView;
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList().Where(endPort =>
                endPort.direction != startPort.direction &&
                endPort.node != startPort.node).ToList();
        }
    }
}