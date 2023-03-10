using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BehaviourTree{
    [CreateAssetMenu(fileName = "Behaviour Tree", menuName = "Behaviour Tree/Behaviour Tree", order = 0)]
    public class BehaviourTree : ScriptableObject{
        public Node root;
        public List<Node> nodes = new();
        [field: SerializeField] public Status Status{ get; protected set; } = Status.Running;

        public BehaviourTree Clone(Dictionary<string, object> treeContext){
            BehaviourTree behaviourTree = Instantiate(this);
            behaviourTree.root = behaviourTree.root.Clone(treeContext);
            return behaviourTree;
        }

        public void Reset(){ }

        public Status Run(){
            if (root.Status == Status.Running){ Status = root.Run(); }

            return Status;
        }

        public void Stop(){ }

#if UNITY_EDITOR
        public Node CreateNode<T>() where T : Node{
            Node node = CreateInstance<T>();
            node.name = typeof(T).Name;
            node.guid = GUID.Generate().ToString();
            nodes.Add(node);

            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
            return node;
        }

        public void DeleteNode(Node node){
            nodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(Node parent, Node child){
            switch (parent){
                case DecoratorNode decoratorNode:
                    decoratorNode.child = child;
                    return;
                case CompositeNode compositeNode:
                    compositeNode.children?.Add(child);
                    return;
                case RootNode rootNode:
                    rootNode.child = child;
                    return;
            }
        }

        public void RemoveChild(Node parent, Node child){
            switch (parent){
                case DecoratorNode decoratorNode:
                    decoratorNode.child = null;
                    return;
                case CompositeNode compositeNode:
                    compositeNode.children?.Remove(child);
                    return;
                case RootNode rootNode:
                    rootNode.child = null;
                    return;
            }
        }

        public List<Node> GetChildren(Node parent){
            List<Node> children = new();
            switch (parent){
                case DecoratorNode decoratorNode:
                    if (decoratorNode.child != null){ children.Add(decoratorNode.child); }
                    break;
                case CompositeNode compositeNode:
                    return compositeNode.children;
                case RootNode rootNode:
                    if (rootNode.child != null){ children.Add(rootNode.child); }
                    break;
            }
            return children;
        }
#endif
    }
}