using UnityEngine;

namespace BehaviourTree{
    public class SetTimeVariableNode : LeafNode{
        [SerializeField] private string variableName;

        protected override void OnStart(){ }

        protected override Status OnUpdate(){
            float time = Time.time;
            if (context.TryAdd(variableName, time)){ return Status.Success; }
            context[variableName] = time;
            return Status.Success;
        }

        protected override void OnStop(){ }
    }
}