using UnityEngine;

namespace BehaviourTree{
    public class DebugNode : LeafNode{
        [SerializeField] private string message;

        protected override void OnStart(){
            Debug.Log($"START: {message}");
        }

        protected override Status OnUpdate(){
            Debug.Log($"UPDATE: {message}");
            return Status = Status.Success;
        }

        protected override void OnStop(){
            Debug.Log($"STOP: {message}");
        }
    }
}