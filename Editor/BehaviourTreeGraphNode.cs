using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace BehaviourTree{
    public class BehaviourTreeGraphNode : UnityEditor.Experimental.GraphView.Node{
        public readonly Node node;
        public Action<BehaviourTreeGraphNode> OnNodeSelected;

        public BehaviourTreeGraphNode(Node behaviourTreeNode){
            node = behaviourTreeNode;
            title = node.name.Replace("Node", "");
            viewDataKey = node.guid;
            style.left = node.nodePosition.x;
            style.top = node.nodePosition.y;

            CreateInputPorts();
            CreateOutputPorts();
        }

        public Port Input{ get; private set; }
        public Port Output{ get; private set; }

        public sealed override string title{ get => base.title; set => base.title = value; }

        private void CreateInputPorts(){
            if (node is RootNode){ return; }

            Input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, null);
            Input.portName = "";
            inputContainer.Add(Input);
        }

        private void CreateOutputPorts(){
            switch (node){
                case LeafNode:
                    return;
                case CompositeNode:
                    Output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, null);
                    break;
                case DecoratorNode:
                    Output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, null);
                    break;
                case RootNode:
                    Output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, null);
                    break;
            }

            Output.portName = "";
            outputContainer.Add(Output);
        }

        public override void SetPosition(Rect newPos){
            base.SetPosition(newPos);
            node.nodePosition.x = newPos.xMin;
            node.nodePosition.y = newPos.yMin;
        }

        public override void OnSelected(){
            base.OnSelected();
            OnNodeSelected?.Invoke(this);
        }
    }
}