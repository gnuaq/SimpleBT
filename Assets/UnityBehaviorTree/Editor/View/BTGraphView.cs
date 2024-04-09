using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityBehaviorTree.Editor.View
{
    public class BTGraphView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<BTGraphView, GraphView.UxmlTraits> { }
        public BTGraphView()
        {
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            GridBackground gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();
            Insert(0, gridBackground);
        }
    }
}