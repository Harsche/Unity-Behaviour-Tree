using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree{
    public class PopFromStack : LeafNode{
        [SerializeField] private string stackName;
        [SerializeField] private string variableName;

        protected override void OnStart(){ }

        protected override Status OnUpdate(){
            if (!TryGetVariable(stackName, out Stack<Component> stack)){ return Status.Failure; }
            SetVariable(variableName, stack.Pop());
            return Status.Success;
        }

        protected override void OnStop(){ }
    }
}