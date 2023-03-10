using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace BehaviourTree{
    public abstract class Node : ScriptableObject{
        protected Dictionary<string, object> context;
        private bool initialized;
        [field: SerializeField] public Status Status{ get; protected set; } = Status.Running;

        protected abstract void OnStart();
        protected abstract Status OnUpdate();
        protected abstract void OnStop();

        protected bool TryGetVariable<T>(string variableName, out T variable){
            if (context.TryGetValue(variableName, out object value)){
                variable = (T) value;
                return true;
            }
            variable = default;
            Debug.LogError($"There is no \"{variableName}\" variable present in the context.");
            return false;
        }

        protected void SetVariable(string variableName, object variable){
            if (context.TryAdd(variableName, variable)){ return; }
            context[variableName] = variable;
        }

        public Status Run(){
            if (!initialized){
                OnStart();
                Status = Status.Running;
                initialized = true;
            }

            Status = OnUpdate();

            if (Status != Status.Success && Status != Status.Failure){ return Status; }

            OnStop();
            initialized = false;

            return Status;
        }

        public virtual Node Clone(Dictionary<string, object> treeContext){
            Node clone = Instantiate(this);
            clone.context = treeContext;
            return clone;
        }

#if UNITY_EDITOR
        public string guid;
        [FormerlySerializedAs("position"), HideInInspector] public Vector2 nodePosition;
#endif
    }

    [Serializable]
    public enum Status{
        Success,
        Failure,
        Running
    }
}