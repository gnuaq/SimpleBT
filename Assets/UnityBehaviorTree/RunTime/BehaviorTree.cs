﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityBehaviorTree.Core.Action;
using UnityBehaviorTree.Core.Composite;
using UnityBehaviorTree.Core.Decorator;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace UnityBehaviorTree.Core
{
    [CreateAssetMenu(fileName = "BehaviorTree", menuName = "BehaviorTree/BehaviorTree")]
    public class BehaviorTree : ScriptableObject
    {
        [SerializeField]
        private List<BTNode> _nodes;
        private RootNode _rootNode;
        [SerializeReference]
        private Blackboard _blackboard;
        
        public List<BTNode> Nodes => _nodes;
        public Blackboard Blackboard => _blackboard;

        public void AddEdge(BTNode inputNode, BTNode outputNode)
        {
            inputNode.NodeViewData.InputNodeID = outputNode.UID;
            outputNode.NodeViewData.OutputNodeIDs.Add(inputNode.UID);
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets(); 
        }
        
        public void RemoveEdge(BTNode inputNode, BTNode outputNode)
        {
            inputNode.NodeViewData.InputNodeID = null;
            outputNode.NodeViewData.OutputNodeIDs.Remove(inputNode.UID);
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets(); 
        }

        public BTNode AddRootNode(Vector2 pos)
        {
            var rootNode = AddNode(typeof(RootNode), pos);
            _rootNode = rootNode as RootNode;
            return rootNode;
        }

        public BTNode AddNode(Type type, Vector2 pos)
        {
            BTNode node = ScriptableObject.CreateInstance(type) as BTNode;
            node.UID = GUID.Generate().ToString();
            node.NodeViewData = new NodeViewData
            {
                Title = type.Name,
                Position = pos,
                OutputNodeIDs = new List<string>(),
            };
            _nodes.Add(node);
            
            if (!Application.isPlaying) {
                AssetDatabase.AddObjectToAsset(node, this);
            }
            
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets(); 
            return node;
        }

        public void RemoveNode(string UID)
        {
            Debug.Log("remove");
            _nodes.Remove(FindNodeByID(UID));
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets(); 
        }

        public void RemoveNode(BTNode node)
        {
            _nodes.Remove(node);
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets(); 
        }

        public void MoveNode(BTNode node, Vector2 pos)
        {
            node.NodeViewData.Position = pos;
        }
        
        public void CreateBlackboard()
        {
            _blackboard = new Blackboard();
            
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets(); 
        }

        public BTNode FindNodeByID(string UID)
        {
            return _nodes.FirstOrDefault(node => node.UID == UID);
        }
        
        public void GenerateTree(GameObject gameObject)
        {
            AddContext(gameObject);
            if (_blackboard == null)
            {
                CreateBlackboard();
            }
            
            foreach (var node in _nodes)
            {
                node.ResetData();
                
                if (node is RootNode rootNode)
                {
                    _rootNode = rootNode;
                }
                foreach (var outputNodeID in node.NodeViewData.OutputNodeIDs)
                {
                    var outNode = FindNodeByID(outputNodeID);
                    AddChild(node, outNode);
                }
            }
            
            foreach (var node in _nodes)
            {
                if (node is CompositeNode compositeNode)
                {
                    compositeNode.Children.Sort(SortByHorizontalPosition);
                }
            }
        }

        public void AddContext(GameObject gameObject)
        {
            TreeTraversel(node => { node.Context = Context.CreateContextFromGameObject(gameObject); });
        }
        
        private void TreeTraversel(System.Action<BTNode> action)
        {
            foreach (var node in _nodes)
            {
                action?.Invoke(node);
            }
        }

        private int SortByHorizontalPosition(BTNode left, BTNode right) {
            return left.NodeViewData.Position.x < right.NodeViewData.Position.x ? -1 : 1;
        }

        public void AddChild(BTNode parent, BTNode child)
        {
            if (parent is DecoratorNode decorator)
            {
                decorator.Child = child;
            }

            if (parent is RootNode rootNode)
            {
                rootNode.Child = child;
            }

            if (parent is CompositeNode composite)
            {
                composite.Children.Add(child);
            }
        }

        public void Tick()
        {
            if (_rootNode.Status == BTNode.EStatus.Running || _rootNode.Status == BTNode.EStatus.None)
            {
                _rootNode.Tick();
            }
        }
    }
}