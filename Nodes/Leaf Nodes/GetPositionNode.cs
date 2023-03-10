using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree{
    public class GetPositionNode : LeafNode{
        [SerializeField] private string variableName;

        protected override void OnStart(){ }

        protected override Status OnUpdate(){
            if (!context.TryGetValue("transform", out object value)){ return Status.Failure; }

            Vector3 position = ((Transform) value).position;
            if (context.TryAdd(variableName, position)){ return Status.Success; }
            context[variableName] = position;
            return Status.Success;
        }

        protected override void OnStop(){ }
    }
}