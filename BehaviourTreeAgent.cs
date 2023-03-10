using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree{
    public class BehaviourTreeAgent : MonoBehaviour{
        [SerializeField] private BehaviourTree behaviourTree;
        [SerializeField] private bool playOnAwake = true;
        private Coroutine behaviourTreeCoroutine;
        public Dictionary<string, object> Context{ get; } = new();

        private void Awake(){
            Context.Add("transform", transform);
            Context.Add("gameObject", gameObject);
        }

        private void Start(){
            if (playOnAwake){ RunBehaviourTree(); }
        }

        [ContextMenu("Run Behaviour Tree")]
        public void RunBehaviourTree(){
            behaviourTree = behaviourTree.Clone(Context);
            behaviourTreeCoroutine = StartCoroutine(BehaviourTreeCoroutine());
        }

        [ContextMenu("Stop Behaviour Tree")]
        public void StopBehaviourTree(){
            if (behaviourTreeCoroutine != null){ StopCoroutine(behaviourTreeCoroutine); }
        }

        private IEnumerator BehaviourTreeCoroutine(){
            while (true){
                behaviourTree.Run();
                yield return null;
            }
            // ReSharper disable once IteratorNeverReturns
        }
    }
}