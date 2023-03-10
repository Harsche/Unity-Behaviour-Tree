using System;
using System.Collections.Generic;

namespace BehaviourTree{
    [Serializable]
    public abstract class CompositeNode : Node{
        public List<Node> children = new();
        public int Current{ get; protected set; }

        public override Node Clone(Dictionary<string, object> treeContext){
            CompositeNode node = Instantiate(this);
            node.children = children.ConvertAll(child => child.Clone(treeContext));
            return node;
        }
    }

    [Serializable]
    public abstract class DecoratorNode : Node{
        public Node child;

        public override Node Clone(Dictionary<string, object> treeContext){
            DecoratorNode node = Instantiate(this);
            node.child = child.Clone(treeContext);
            return node;
        }
    }

    [Serializable]
    public abstract class LeafNode : Node{ }
}