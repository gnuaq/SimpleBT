﻿using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityBehaviorTree.Core
{
    [System.Serializable]
    public class NodeViewData
    {
        public string Title;
        public string Comment;
        
        public Vector2 Position;
        public string InputNodeID;
        public List<string> OutputNodeIDs;
    }

    public struct PortConf
    {
        public bool HasInputPort;
        public Port.Capacity InputPortCapacity;
        public bool HasOutputPort;
        public Port.Capacity OutputPortcapacity;
    }
    
    [System.Serializable]
    public abstract class BTNode : ScriptableObject
    {
        public enum EStatus
        {
            None,
            Running,
            Failed,
            Success,
        }

        private bool _started;
        [SerializeReference]
        private NodeViewData _nodeViewData;

        [SerializeField]
        protected EStatus _status;
        [SerializeField]
        protected PortConf _portConf;

        public System.Action<NodeViewData> OnNodeDataChange;
        
        public string UID;
        public Context Context;
        public Blackboard Blackboard;
        public EStatus Status => _status;
        public bool Started => _started;
        public NodeViewData NodeViewData
        {
            get => _nodeViewData;
            set => _nodeViewData = value;
        }
        public PortConf PortConf => _portConf;

        public BTNode()
        {
            InitPort();
            _status = EStatus.None;
            _started = false;
        }

        public void Tick()
        {
            if (!_started)
            {
                OnStart();
                _started = true;
            }

            _status = OnUpdate();

            if (_status != EStatus.Running)
            {
                OnStop();
                _started = false;
            }
        }

        private void OnValidate()
        {
            OnNodeDataChange?.Invoke(_nodeViewData);
        }

        protected void Attach(BTNode node)
        {
            
        }
        
        public virtual void ResetData()
        {
            _started = false;
        }

        protected abstract void InitPort();
        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract EStatus OnUpdate();
        public abstract BTNode Clone();
    }
}