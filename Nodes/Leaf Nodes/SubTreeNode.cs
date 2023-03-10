using UnityEngine;

namespace BehaviourTree{
    public class SubTreeNode : LeafNode{
        [SerializeField] private BehaviourTree subTree;

        protected override void OnStart(){ }

        protected override Status OnUpdate(){
            subTree = subTree.Clone(context);
            return subTree.Run();
        }

        protected override void OnStop(){ }
    }
}