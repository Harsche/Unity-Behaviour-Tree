using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace BehaviourTree{
    public class BehaviourTreeEditor : EditorWindow{
        private BehaviourTreeView behaviourTreeView;
        private InspectorView inspectorView;

        public void CreateGUI(){
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Assets/Harsche Inter-6 develop Assets-_Game_Scripts_Features_IA/Editor/BehaviourTreeEditor.uxml");
            visualTree.CloneTree(root);

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(
                "Assets/Harsche Inter-6 develop Assets-_Game_Scripts_Features_IA/Editor/BehaviourTreeEditor.uss");
            root.styleSheets.Add(styleSheet);

            behaviourTreeView = root.Q<BehaviourTreeView>();
            inspectorView = root.Q<InspectorView>();
            behaviourTreeView.OnNodeSelected = OnNodeSelectionChanged;
            OnSelectionChange();
        }

        private void OnSelectionChange(){
            var behaviourTree = Selection.activeObject as BehaviourTree;
            if (behaviourTree && AssetDatabase.CanOpenAssetInEditor(behaviourTree.GetInstanceID())){
                behaviourTreeView.PopulateView(behaviourTree);
            }
        }

        [MenuItem("Window/BehaviourTreeEditor/Editor")]
        public static void OpenWindow(){
            var wnd = GetWindow<BehaviourTreeEditor>();
            wnd.titleContent = new GUIContent("BehaviourTreeEditor");
        }

        private void OnNodeSelectionChanged(BehaviourTreeGraphNode behaviourTreeGraphNode){
            inspectorView.UpdateSelection(behaviourTreeGraphNode);
        }
    }
}