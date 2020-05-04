using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IL.Simulation
{
    public class AIAgent : MonoBehaviour
    {
        [Header("Teaching environment and brain")]
        public ClassRoom classRoom;
        public StudentBrain brain;

        [Header("Agent Settings")]
        public string agentType;
        public long timeAlive;

        [Header("DEBUG - for navigation")]
        public AIState currentState;
        public AIState destination;
        public AIAction action;
        public AIAction action2;
        public bool paused;
        public int pausing;       
        
    }
}
