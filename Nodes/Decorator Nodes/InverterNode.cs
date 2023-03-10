using System;

namespace BehaviourTree{
    public class InverterNode : DecoratorNode{
        protected override void OnStart(){ }

        protected override Status OnUpdate(){
            return child.Run() switch{
                Status.Success => Status.Failure,
                Status.Failure => Status.Success,
                Status.Running => Status.Running,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        protected override void OnStop(){ }
    }
}