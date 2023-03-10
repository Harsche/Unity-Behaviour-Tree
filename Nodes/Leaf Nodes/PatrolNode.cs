using BehaviourTree;
using UnityEngine;
using UnityEngine.AI;

public class PatrolNode : LeafNode{
    [SerializeField] private float distance;
    [SerializeField] private bool waitForCompletion;

    protected override void OnStart(){ }

    protected override Status OnUpdate(){
        if (!context.TryGetValue("navMeshAgent", out object value)){
            Debug.LogError("There is no NavMeshAgent present in context");
            return Status.Failure;
        }

        var agent = (NavMeshAgent) value;
        if (!agent || !agent.isOnNavMesh){ return Status.Failure; }
        if (waitForCompletion && agent.hasPath){
            return agent.remainingDistance <= 0.1f ? Status.Success : Status.Running;
        }

        Vector3 destination = (Vector3) context["startPosition"] +
                              Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up) * Vector3.right * distance;
        NavMeshPath navMeshPath = new();
        if (!agent.CalculatePath(destination, navMeshPath) ||
            navMeshPath.status != NavMeshPathStatus.PathComplete){ return Status.Failure; }
        agent.SetPath(navMeshPath);
        return waitForCompletion ? Status.Running : Status.Success;
    }

    protected override void OnStop(){ }
}