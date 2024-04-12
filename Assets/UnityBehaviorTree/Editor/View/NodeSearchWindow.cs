using System;
using System.Collections.Generic;
using UnityBehaviorTree.Core;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace UnityBehaviorTree.Editor.View
{
    public class NodeSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        private BTGraphView _graphView;
        private BTEditorWindow _editorWindow;

        public BTGraphView BTGraphView
        {
            set => _graphView = value;
            get => _graphView;
        }
        
        public BTEditorWindow BTEditorWindow
        {
            set => _editorWindow = value;
            get => _editorWindow;
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var tree = new List<SearchTreeEntry>
            {
                new SearchTreeGroupEntry(new GUIContent("Create Node"), 0),
            };

            {
                var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
                tree.Add(new SearchTreeGroupEntry(new GUIContent("Composite Node"), 1));
                foreach (var type in types)
                {
                    tree.Add(new SearchTreeEntry(new GUIContent(type.Name))
                    {
                        level = 2,
                        userData = type
                    });
                }
            }

            {
                var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
                tree.Add(new SearchTreeGroupEntry(new GUIContent("Decorator Node"), 1));
                foreach (var type in types)
                {
                    tree.Add(new SearchTreeEntry(new GUIContent(type.Name))
                    {
                        level = 2,
                        userData = type
                    });
                }
            }
            
            {
                var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
                tree.Add(new SearchTreeGroupEntry(new GUIContent("Action Node"), 1));
                foreach (var type in types)
                {
                    tree.Add(new SearchTreeEntry(new GUIContent(type.Name))
                    {
                        level = 2,
                        userData = type
                    });
                }
            }
            
            {
                tree.Add(new SearchTreeEntry(new GUIContent("RootNode"))
                {
                    level = 1,
                    userData = typeof(RootNode)
                });
            }

            return tree;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            var type = SearchTreeEntry.userData as Type;
            if (type == typeof(RootNode))
                _graphView.AddRootNote(Vector2.zero);
            else
                _graphView.AddNode(type, Vector2.zero);
            return true;
        }
    }
}