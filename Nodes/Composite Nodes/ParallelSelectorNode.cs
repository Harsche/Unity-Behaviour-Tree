using System;

namespace BehaviourTree{
    public class ParallelSelectorNode : CompositeNode{
        protected override void OnStart(){
        }

        protected override Status OnUpdate(){
            foreach (Node child in children)
                switch (child.Run()){
                    case Status.Success:
                        return Status.Success;
                    case Status.Failure:
                        Current++;
                        continue;
                    case Status.Running:
                        Current++;
                        continue;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            return Status.Failure;
        }

        protected override void OnStop(){
        }
    }
}