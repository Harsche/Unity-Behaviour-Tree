using UnityEngine;

namespace BehaviourTree{
    public class IsObjectWithinRangeNode : LeafNode{
        [SerializeField] private string objectVariableName;
        [SerializeField] private string rangeVariableName;

        protected override void OnStart(){ }

        protected override Status OnUpdate(){
            if (!TryGetVariable(objectVariableName, out Component component)){ return Status.Failure; }
            if (!TryGetVariable(rangeVariableName, out float range)){ return Status.Failure; }
            if (!TryGetVariable("transform", out Transform transform)){ return Status.Failure; }

            return Vector3.Distance(transform.position, component.transform.position) <= range
                ? Status.Success
                : Status.Failure;
        }

        protected override void OnStop(){ }
    }
}