using System;
using UnityEngine;

namespace VketTools.Utilities
{
    public class SequenceInfo : ScriptableObject
    {
        public SequenceStatus[] sequenceStatus;
    }

    [Serializable]
    public class SequenceStatus
    {
        public enum RunStatus
        {
            None = 0,
            Running = 1,
            Complete = 2,
        }
        
        public RunStatus status = RunStatus.None;
        public string desc = "";
    }
}