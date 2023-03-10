using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree{
    [CreateAssetMenu(menuName = "Behaviour Tree/Leaf Nodes/Debug Node", fileName = "Debug Node", order = 0)]
    public class DebugNode : LeafNode{
        protected override void OnStart(){
            Debug.Log("START");
        }

        protected override Status OnUpdate(){
            Debug.Log("UPDATE");
            return Status = Status.Success;
        }

        protected override void OnStop(){
            Debug.Log("STOP");
        }
    }
}