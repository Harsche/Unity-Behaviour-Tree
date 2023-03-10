using UnityEngine;

namespace BehaviourTree{
    public class RepeaterNode : DecoratorNode{
        [SerializeField] private int repeat;
        private int current;

        protected override void OnStart(){
            current = 0;
        }

        protected override Status OnUpdate(){
            if (repeat < 0){
                child.Run();
                return Status.Running;
            }
            if (child.Run() == Status.Running){ return Status.Running; }
            current++;
            return current == repeat ? Status.Success : Status.Running;
        }

        protected override void OnStop(){ }
    }
}