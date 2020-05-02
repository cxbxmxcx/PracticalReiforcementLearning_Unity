using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IL.Simulation
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class StudentAgent : MonoBehaviour
    {
        [Header("Teaching environment and brain")]
        public ClassRoom classRoom;
        public StudentBrain brain;

        [Header("Movement Parameters")]
        public float rotationSpeed = 2;
        public string agentType;        
        public long timeAlive;

        private NavMeshAgent agent;
        public NavMeshAgent Agent
        {
            get
            {
                return agent;
            }
        }
        private Animator animator;
        public Animator Animator
        {
            get
            {
                return animator;
            }
        }

        [Header("DEBUG - for navigation")]
        public AIState currentState;
        public AIState destination;
        public AIAction action;
        public AIAction action2;
        public bool paused;
        public int pausing;        

        //public AIBrain brain;
        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            timeAlive++;

            if (agent == null) return;

            if (paused && pausing-- > 0)
            {
                agent.isStopped = true;
                animator.SetBool("Walking", false);
                RotateTowards(currentState.transform.position);
                return;
            }
            else
            {
                paused = false;
                agent.isStopped = false;
                animator.SetBool("Walking", true);
            }

            if (agent.pathPending == false && agent.remainingDistance < 0.5f)
            {
                Step();
            }
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
            agent.destination = destination.transform.position;
            action2 = brain.Act(destination);
        }

        private void OnTriggerEnter(Collider other)
        {            
            if (other.tag == "Untagged") return; //not anything we want to pay attention to
            if (other.tag == "AIState")
            {
                var state = other.gameObject.GetComponent<AIState>();
                if (state == destination)
                {
                    brain.Learn(currentState, destination, destination.reward, action, action2);
                    currentState = destination;
                    if (currentState.resetAgent)
                    {
                        classRoom.ResetAgent(this);
                        return;
                    }
                    var pause = Random.Range(1, 10);
                    if (paused) pausing = pause;
                    else
                    {
                        paused = true;
                        pausing = pause;
                    }
                }
            }
        }

        private void RotateTowards(Vector3 target)
        {
            Vector3 direction = (target - transform.position).normalized;
            if (direction == Vector3.zero) return;
            //need to reorient rotation to account for model rotation
            Quaternion lookRotation = Quaternion.LookRotation(direction) * Quaternion.AngleAxis(-90, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
