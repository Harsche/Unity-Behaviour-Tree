using System;
using System.Collections.Generic;

namespace BehaviourTree{
    [Serializable]
    public class RootNode : Node{
        public Node child;

        protected override void OnStart(){ }

        protected override Status OnUpdate(){
            return child.Run();
        }

        protected override void OnStop(){ }

        public override Node Clone(Dictionary<string, object> treeContext){
            RootNode node = Instantiate(this);
            node.child = child.Clone(treeContext);
            return node;
        }
    }
}