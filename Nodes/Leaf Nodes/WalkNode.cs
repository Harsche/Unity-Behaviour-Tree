using UnityEngine;
using UnityEngine.AI;

namespace BehaviourTree{
    public class WalkNode : LeafNode{
        [SerializeField] private string variableName;
        [SerializeField] private bool waitForCompletion;

        protected override void OnStart(){ }

        protected override Status OnUpdate(){
            if (!TryGetVariable("navMeshAgent", out NavMeshAgent agent)){ return Status.Failure; }

            if (!agent.isOnNavMesh){ return Status.Failure; }
            if (waitForCompletion && agent.hasPath){
                return agent.remainingDistance <= 0.1f ? Status.Success : Status.Running;
            }

            TryGetVariable(variableName, out Component target);
            Vector3 destination = target.transform.position;
            NavMeshPath navMeshPath = new();
            if (!agent.CalculatePath(destination, navMeshPath) ||
                navMeshPath.status != NavMeshPathStatus.PathComplete){ return Status.Failure; }
            agent.SetPath(navMeshPath);
            return waitForCompletion ? Status.Running : Status.Success;
        }

        protected override void OnStop(){ }
    }
}