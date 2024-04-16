﻿using System;
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
            _editorContainer.Add(_blackboard);

            _graphView = _editorContainer.Q<BTGraphView>();
            _graphView.SetEditorWindow(this);
            _graphView.styleSheets.Add(_editorConfig.GraphViewStyleSheet);
            _graphView.Blackboard = _blackboard;

            _toolbar = _editorContainer.Q<Toolbar>();

            root.Add(_editorContainer);
        }

        public void LoadBehaviorTree(int instanceID)
        {
            string assetPath = AssetDatabase.GetAssetPath(instanceID);
            _graphView.BehaviorTree = AssetDatabase.LoadAssetAtPath<BehaviorTree>(assetPath);
            _graphView.LoadGraph();
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