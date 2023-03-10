using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BehaviourTree{
    public class GetEnemiesOnSight : LeafNode{
        [SerializeField] private string stackName = "enemyStack";
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private string[] enemyTags;
        private readonly Collider[] results = new Collider[10];

        protected override void OnStart(){ }

        protected override Status OnUpdate(){
            TryGetVariable("transform", out Transform transform);
            TryGetVariable("sightRange", out float sightRange);


            int size = Physics.OverlapSphereNonAlloc(transform.position, sightRange, results, layerMask.value,
                QueryTriggerInteraction.Ignore);

            if (size == 0){ return Status.Failure; }

            List<Component> enemies = new();
            TryGetVariable("raycastOrigin", out Transform raycastOrigin);

            for (int i = 0; i < size; i++){
                Collider collider = results[i];
                Vector3 raycastOriginPosition = raycastOrigin.position;
                Ray ray = new(raycastOriginPosition, collider.transform.position - raycastOriginPosition);
                if (Physics.Raycast(ray, sightRange, ~0, QueryTriggerInteraction.Ignore)){
                    enemies.Add(collider.transform);
                }
            }
            enemies = enemies.Where(collider => enemyTags.Any(collider.CompareTag)).ToList();
            Stack<Component> enemyStack = new(enemies);
            SetVariable(stackName, enemyStack);

            return enemyStack.Count > 0 ? Status.Success : Status.Failure;
        }

        protected override void OnStop(){ }
    }
}