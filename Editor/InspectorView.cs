using UnityEditor;
using UnityEngine.UIElements;

namespace BehaviourTree{
    public class InspectorView : VisualElement{
        private Editor editor;
        public InspectorView(){ }
        
        public new class UxmlFactory : UxmlFactory<InspectorView, UxmlTraits>{ }

        public void UpdateSelection(BehaviourTreeGraphNode behaviourTreeGraphNode){
            Clear();
            
            UnityEngine.Object.DestroyImmediate(editor);
            editor = Editor.CreateEditor(behaviourTreeGraphNode.node);
            IMGUIContainer container = new(() => editor.OnInspectorGUI());
            Add(container);
        }
    }
}