namespace BehaviourTree{
    public class SucceederNode : DecoratorNode{
        protected override void OnStart(){ }

        protected override Status OnUpdate(){
            return child.Run() != Status.Running ? Status.Success : Status.Running;
        }

        protected override void OnStop(){ }
    }
}