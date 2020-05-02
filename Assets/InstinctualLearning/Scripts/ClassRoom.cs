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
            //if (masterBrain == null)
            //{
            //    Debug.LogError("No master brain set on AIManager");
            //    return;
            //}
            states = GetComponentsInChildren<AIState>();
            
        }

        // Update is called once per frame
        //void Update()
        //{
        //    if(agentRunning == false && agent == null)
        //    {
        //        CreateAgent();
        //    }
        //}

        public void ResetAgent(StudentAgent agent)
        {            
            //agentRunning = false;
            agent.transform.position = startState.transform.position.Copy();
            agent.currentState = startState;
            agent.destination = null;
            agent.Agent.destination = startState.transform.position.Copy();
        }

        //private void CreateAgent()
        //{
        //    if (agentPrefab == null) Debug.LogError("No agent prefab set on AIManager");

        //    agent = Instantiate<AIAgent>(agentPrefab, transform);
        //    agent.brain = masterBrain;
        //    agentRunning = true;
        //}
    }
}
