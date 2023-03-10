using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

namespace BehaviourTree{
    public class BehaviourTreeEditor : EditorWindow{
        [SerializeField] private VisualTreeAsset visualTreeAsset;
        [SerializeField] private StyleSheet styleSheetAsset;
        private BehaviourTreeView behaviourTreeView;
        private InspectorView inspectorView;

        public void CreateGUI(){
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Import UXML
            VisualTreeAsset visualTree = visualTreeAsset;
            visualTree.CloneTree(root);

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            StyleSheet styleSheet = styleSheetAsset;
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

        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceId, int line){
            if (Selection.activeObject is not BehaviourTree){ return false; }
            OpenWindow();
            return true;
        }

        private void OnNodeSelectionChanged(BehaviourTreeGraphNode behaviourTreeGraphNode){
            inspectorView.UpdateSelection(behaviourTreeGraphNode);
        }
    }
}