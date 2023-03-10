using UnityEngine;

namespace BehaviourTree{
    public class SetNullNode : LeafNode{
        [SerializeField] private string variableName;

        protected override void OnStart(){ }

        protected override Status OnUpdate(){
            if (context.TryAdd(variableName, null)){ return Status.Success; }
            context[variableName] = null;
            return Status.Success;
        }

        protected override void OnStop(){ }
    }
}