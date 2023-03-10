using System;

namespace BehaviourTree{
    public class ParallelCompleteNode : CompositeNode{
        protected override void OnStart(){ }

        protected override Status OnUpdate(){
            foreach (Node child in children){
                switch (child.Run()){
                    case Status.Success:
                        return Status.Success;
                    case Status.Failure:
                        return Status.Failure;
                    case Status.Running:
                        Current++;
                        continue;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return Status.Running;
        }

        protected override void OnStop(){ }
    }
}