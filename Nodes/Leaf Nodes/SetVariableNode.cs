using UnityEngine;

namespace BehaviourTree{
    public class SetVariableNode : LeafNode{
        [SerializeField] private string variableName;
        [SerializeField] private string variableToSet;
        protected override void OnStart(){ }

        protected override Status OnUpdate(){
            if (!TryGetVariable(variableToSet, out object variable)){ return Status.Failure; }
            SetVariable(variableName, variable);
            return Status.Success;
        }

        protected override void OnStop(){ }
    }
}