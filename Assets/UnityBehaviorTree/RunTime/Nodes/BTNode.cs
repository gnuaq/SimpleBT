using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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
        [HideInInspector]
        public string InputNodeID;
        [HideInInspector]
        public List<string> OutputNodeIDs;
    }
    
    public enum PortCapacity
    {
        Single,
        Multi,
    }

    public struct PortConf
    {
        public bool HasInputPort;
        public PortCapacity InputPortCapacity;
        public bool HasOutputPort;
        public PortCapacity OutputPortcapacity;
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
        [SerializeField]
        private EStatus _status;
        [SerializeField]
        private NodeViewData _nodeViewData;
        
        [SerializeField]
        protected PortConf _portConf;

        public System.Action<NodeViewData> OnNodeDataChange;
        public System.Action<EStatus> OnStatusChange;
        
        public string UID;
        public Context Context;
        public Blackboard Blackboard;
        public EStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnStatusChange?.Invoke(_status);
            }
        }

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

            Status = OnUpdate();

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

        public string GetNodeType()
        {
            string[] s = this.GetType().ToString().Split(".");
            return s.LastOrDefault();
        }
        
        public virtual void ResetData()
        {
            Status = EStatus.None;
            _started = false;
        }
        
        public virtual void ResetRunningData()
        {
            Status = EStatus.None;
            _started = false;
        }

        public virtual BTNode Clone()
        {
            var node = Instantiate(this);
            node._nodeViewData = _nodeViewData;
            node._portConf = PortConf;
            return node;
        }

        protected abstract void InitPort();
        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract EStatus OnUpdate();
    }
}