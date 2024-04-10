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

            nodeCreationRequest += context =>
            {
                AddNode(new Wait(3));
            };

            graphViewChanged += OnGraphViewChanged;
        }
        
        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if (graphViewChange.elementsToRemove != null)
            {
                
            }
            return graphViewChange;
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