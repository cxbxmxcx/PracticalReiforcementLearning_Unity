using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IL.Simulation
{
    public class ClassRoom : MonoBehaviour
    {
        public AIState startState;
        public AIState[] states;
          
        // Start is called before the first frame update
        void Start()
        {            
            states = GetComponentsInChildren<AIState>(); 
        }
        
        public void ResetAgent(AIAgent agent)
        {            
            //agentRunning = false;
            agent.transform.position = startState.transform.position.Copy();
            agent.currentState = startState;
            agent.destination = null;
            (agent as StudentAgent)?.Agent.SetDestination(startState.transform.position.Copy());             
        }        
    }
}
