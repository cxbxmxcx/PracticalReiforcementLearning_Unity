using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace IL.Simulation
{    
    public class PlanningAgent : AIAgent
    {   
        // Update is called once per frame
        void Update()
        {
            timeAlive++;           
            Step();           
        }

        private void Step()
        {
            //if (brain == null) return;
            if(currentState == null)
            {
                //lost, need to move to home state or closet state
                currentState = classRoom.startState;
            }
            
            action = brain.Act(currentState);
            if (action == null) return;
            destination = action.nextState;
            action2 = brain.Act(destination);
            brain.Learn(currentState, destination, destination.reward, action, action2);
            
            if (destination.resetAgent)
            {
                classRoom.ResetAgent(this);
            }
            else
            {
                transform.position = destination.transform.position.Copy();
                currentState = destination;
            }
        }

        private void OnDrawGizmos()
        {
            Handles.Label(transform.position, name);
        }

        //private void OnTriggerEnter(Collider other)
        //{            
        //    if (other.tag == "Untagged") return; //not anything we want to pay attention to
        //    if (other.tag == "AIState")
        //    {
        //        var state = other.gameObject.GetComponent<AIState>();
        //        if (state == destination)
        //        {
        //            brain.Learn(currentState, destination, destination.reward, action, action2);
        //            currentState = destination;
        //            if (currentState.resetAgent)
        //            {
        //                classRoom.ResetAgent(this);
        //                return;
        //            }
        //            var pause = Random.Range(1, 10);
        //            if (paused) pausing = pause;
        //            else
        //            {
        //                paused = true;
        //                pausing = pause;
        //            }
        //        }
        //    }
        //}


    }
}
