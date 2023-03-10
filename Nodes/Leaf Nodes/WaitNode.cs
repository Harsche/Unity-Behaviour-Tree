using System;
using UnityEngine;

namespace BehaviourTree{
    public class WaitNode : LeafNode{
        [Serializable]
        public enum WaitTimeMode{
            VariableTime,
            NodeStartTime
        }

        [SerializeField] private WaitTimeMode waitTimeMode;
        [SerializeField] private string timeVariable;
        [SerializeField] private float duration;
        private float startTime;

        protected override void OnStart(){
            if (waitTimeMode == WaitTimeMode.NodeStartTime){ startTime = Time.time; }
        }

        protected override Status OnUpdate(){
            return waitTimeMode switch{
                WaitTimeMode.VariableTime => CalculateVariableTime(),
                WaitTimeMode.NodeStartTime => CalculateNodeStartTime(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        protected override void OnStop(){ }

        private Status CalculateNodeStartTime(){
            return Time.time < startTime + duration ? Status.Running : Status.Success;
        }

        private Status CalculateVariableTime(){
            if (!TryGetVariable(timeVariable, out float time)){ return Status.Failure; }
            return Time.time < time + duration ? Status.Failure : Status.Success;
        }
    }
}