using UnityEngine;

namespace BehaviourTree{
    public class IsNullNode : LeafNode{
        [SerializeField] private string variableName;

        protected override void OnStart(){ }

        protected override Status OnUpdate(){
            if (!TryGetVariable(variableName, out object variable)){ return Status.Failure; }
            return variable == null ? Status.Success : Status.Failure;
        }

        protected override void OnStop(){ }
    }
}