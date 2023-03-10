using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace BehaviourTree{
    public class BehaviourTreeView : GraphView{
        private BehaviourTree behaviourTree;
        public Action<BehaviourTreeGraphNode> OnNodeSelected;

        public BehaviourTreeView(){
            Insert(0, new GridBackground());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(
                "Assets/Harsche Inter-6 develop Assets-_Game_Scripts_Features_IA/Editor/BehaviourTreeEditor.uss");
            styleSheets.Add(styleSheet);
        }

        public void PopulateView(BehaviourTree tree){
            behaviourTree = tree;

            graphViewChanged -= GraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += GraphViewChanged;

            if (behaviourTree.root == null){
                behaviourTree.root = behaviourTree.CreateNode<RootNode>();
                EditorUtility.SetDirty(behaviourTree);
                AssetDatabase.SaveAssets();
            }

            behaviourTree.nodes.ForEach(CreateGraphNode);
            
            behaviourTree.nodes.ForEach(CreateNodeEdges);
        }

        private void CreateNodeEdges(Node node){
            List<Node> children = behaviourTree.GetChildren(node);
            children?.ForEach(child => {
                 BehaviourTreeGraphNode parentNode = GetGraphNode(node);
                 BehaviourTreeGraphNode childNode = GetGraphNode(child);
                 
                 Edge edge = parentNode.Output.ConnectTo(childNode.Input);
                 AddElement(edge);
            });
        }

        private BehaviourTreeGraphNode GetGraphNode(Node node){
            return GetNodeByGuid(node.guid) as BehaviourTreeGraphNode;
        }

        private new GraphViewChange GraphViewChanged(GraphViewChange graphViewChange){
            graphViewChange.elementsToRemove?.ForEach(element => {
                if (element is BehaviourTreeGraphNode graphNode){ behaviourTree.DeleteNode(graphNode.node); }
                else if (element is Edge edge){
                    var parentNode = edge.output.node as BehaviourTreeGraphNode;
                    var childNode = edge.input.node as BehaviourTreeGraphNode;
                    behaviourTree.RemoveChild(parentNode?.node, childNode?.node);
                }
            });

            graphViewChange.edgesToCreate?.ForEach(edge => {
                var parentNode = edge.output.node as BehaviourTreeGraphNode;
                var childNode = edge.input.node as BehaviourTreeGraphNode;
                behaviourTree.AddChild(parentNode?.node, childNode?.node);
            });

            return graphViewChange;
        }

        private void CreateGraphNode(Node node){
            var graphViewNode = new BehaviourTreeGraphNode(node){
                OnNodeSelected = OnNodeSelected
            };
            AddElement(graphViewNode);
        }

        private void CreateNode<T>() where T : Node{
            Node node = behaviourTree.CreateNode<T>();
            CreateGraphNode(node);
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt){
            TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom(typeof(CompositeNode));
            foreach (Type type in types){
                evt.menu.AppendAction(
                    $"[{type.BaseType?.Name.Replace("Node", "")}] {type.Name.Replace("Node", "")}",
                    _ => {
                        MethodInfo createNodeMethod = typeof(BehaviourTreeView).GetMethod(nameof(CreateNode),
                            BindingFlags.NonPublic | BindingFlags.Instance);
                        createNodeMethod = createNodeMethod?.MakeGenericMethod(type);
                        createNodeMethod?.Invoke(this, null);
                    });
            }

            types = TypeCache.GetTypesDerivedFrom(typeof(DecoratorNode));
            foreach (Type type in types){
                evt.menu.AppendAction(
                    $"[{type.BaseType?.Name.Replace("Node", "")}] {type.Name.Replace("Node", "")}",
                    _ => {
                        MethodInfo createNodeMethod = typeof(BehaviourTreeView).GetMethod(nameof(CreateNode),
                            BindingFlags.NonPublic | BindingFlags.Instance);
                        createNodeMethod = createNodeMethod?.MakeGenericMethod(type);
                        createNodeMethod?.Invoke(this, null);
                    });
            }

            types = TypeCache.GetTypesDerivedFrom(typeof(LeafNode));
            foreach (Type type in types){
                evt.menu.AppendAction(
                    $"[{type.BaseType?.Name.Replace("Node", "")}] {type.Name.Replace("Node", "")}",
                    _ => {
                        MethodInfo createNodeMethod = typeof(BehaviourTreeView).GetMethod(nameof(CreateNode),
                            BindingFlags.NonPublic | BindingFlags.Instance);
                        createNodeMethod = createNodeMethod?.MakeGenericMethod(type);
                        createNodeMethod?.Invoke(this, null);
                    });
            }
            // base.BuildContextualMenu(evt);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter){
            return ports.Where(endPort =>
                endPort.direction != startPort.direction && endPort.node != startPort.node).ToList();
        }

        public new class UxmlFactory : UxmlFactory<BehaviourTreeView, UxmlTraits>{ }
    }
}