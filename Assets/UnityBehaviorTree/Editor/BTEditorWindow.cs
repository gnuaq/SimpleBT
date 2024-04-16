using System;
using System.Collections.Generic;
using System.Linq;
using UnityBehaviorTree.Core;
using UnityBehaviorTree.Editor.View;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityBehaviorTree.Editor
{
    public class BTEditorWindow : EditorWindow
    {
        private BTEditorConfig _editorConfig;
        private VisualElement _editorContainer;
        private BTGraphView _graphView;
        private Toolbar _toolbar;
        private BTBlackboard _blackboard;
        
        [MenuItem("BehaviorTree/OpenBehaviorTreeWindow")]
        public static void ShowBehaviorTreeWindow()
        {
            BTEditorWindow wnd = GetWindow<BTEditorWindow>();
            wnd.titleContent = new GUIContent("BehaviorTree Window");

            wnd.minSize = new Vector2(450, 200);
            wnd.maxSize = new Vector2(1920, 720);
        }

        public void CreateGUI()
        {
            LoadConfigAsset();
            
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Instantiate UXML
            _editorContainer = _editorConfig.GraphViewVirtVisualTreeAsset.Instantiate();
            _editorContainer.StretchToParentSize();
            
            _blackboard = new BTBlackboard();
            // _editorContainer.Add(_blackboard);

            _graphView = _editorContainer.Q<BTGraphView>();
            _graphView.SetEditorWindow(this);
            _graphView.styleSheets.Add(_editorConfig.GraphViewStyleSheet);
            _graphView.Blackboard = _blackboard;

            _toolbar = _editorContainer.Q<Toolbar>();
            InitToolBarButton();
            
            root.Add(_editorContainer);
        }

        private void InitToolBarButton()
        {
            // _toolbar.Add(CreateButton("SaveGraph", "", () => _graphView.SaveGraph()));
            _toolbar.Add(CreateButton("ResetTree", "", ResetTree));
            _toolbar.Add(CreateButton("Setting", "", Setting));
        }

        public static VisualElement CreateButton(string name, string tooltip, Action clickEvent)
        {
            var button = new Button(clickEvent);
            button.text = name;
            button.tooltip = tooltip;
            return button;
        }

        private void ResetTree()
        {
            _graphView.BehaviorTree.ResetData();
        }

        private void Setting()
        {
            
        }

        public void LoadBehaviorTree(int instanceID)
        {
            string assetPath = AssetDatabase.GetAssetPath(instanceID);
            var bt = AssetDatabase.LoadAssetAtPath<BehaviorTree>(assetPath);
            _graphView.LoadGraph(bt);
        }

        public void LoadConfigAsset()
        {
            string guid = AssetDatabase.FindAssets("t:BTEditorConfig").FirstOrDefault();
            if (guid.Length > 1)
            {
                Debug.LogWarning($"Found multiple settings files, using the first.");
            }
            BTEditorConfig editorConfig = AssetDatabase.LoadAssetAtPath<BTEditorConfig>(AssetDatabase.GUIDToAssetPath(guid));
            _editorConfig = editorConfig;
        }
    }
}