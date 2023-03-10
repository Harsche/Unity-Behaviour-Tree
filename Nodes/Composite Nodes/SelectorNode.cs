using System;

namespace BehaviourTree{
    public class SelectorNode : CompositeNode{
        protected override void OnStart(){ }

        protected override Status OnUpdate(){
            for (int i = Current; i < children.Count; i++)
                switch (children[i].Run()){
                    case Status.Success:
                        return Status.Success;
                    case Status.Failure:
                        Current++;
                        continue;
                    case Status.Running:
                        return Status.Running;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            return Status.Failure;
        }

        protected override void OnStop(){
            Current = 0;
        }
    }
}