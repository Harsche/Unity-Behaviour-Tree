using System;

namespace BehaviourTree{
    public class ParallelSequenceNode : CompositeNode{
        protected override void OnStart(){ }

        protected override Status OnUpdate(){
            foreach (Node child in children){
                switch (child.Run()){
                    case Status.Success:
                        Current++;
                        continue;
                    case Status.Failure:
                        return Status.Failure;
                    case Status.Running:
                        Current++;
                        continue;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return Status.Success;
        }

        protected override void OnStop(){ }
    }
}