using UnityEngine;

namespace BehaviourTree{
    public class RepeatUntilFailNode : DecoratorNode{
        [SerializeField] private bool executeOnSameFrame;

        protected override void OnStart(){ }

        protected override Status OnUpdate(){
            if (!executeOnSameFrame){ return child.Run() != Status.Failure ? Status.Running : Status.Success; }

            do{ Status = child.Run(); } while (Status != Status.Failure);
            return Status.Success;
        }

        protected override void OnStop(){ }
    }
}