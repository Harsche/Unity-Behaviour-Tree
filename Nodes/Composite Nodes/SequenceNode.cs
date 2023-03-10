using System;

namespace BehaviourTree{
    public class SequenceNode : CompositeNode{
        protected override void OnStart(){ }

        protected override Status OnUpdate(){
            for (int i = Current; i < children.Count; i++)
                switch (children[i].Run()){
                    case Status.Success:
                        Current++;
                        continue;
                    case Status.Failure:
                        return Status.Failure;
                    case Status.Running:
                        return Status.Running;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            return Status.Success;
        }

        protected override void OnStop(){
            Current = 0;
        }
    }
}