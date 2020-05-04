using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IL.Simulation
{
    [RequireComponent(typeof(MeshRenderer))]
    public class SelfOrganizingState : AIState
    {        
        public List<AIState> connectedStates;       
        public float detectRadius;
        public MeshRenderer mesh;

        private void Start()
        {
            mesh = GetComponent<MeshRenderer>();
        }

        public override void SetMaterial(Material mat)
        {
            if (mesh == null || mat == null) return;

            mesh.material = mat;
        }

        //initialize the state of the object
        private void Awake()
        {
            stateName = name;
            actions = new List<AIAction>();
            connectedStates = new List<AIState>();
            var colliders = Physics.OverlapSphere(transform.position, detectRadius);
            foreach(var collider in colliders)
            {
                if (collider.tag == "Untagged" || collider.gameObject == gameObject) continue;
                var state = collider.gameObject.GetComponent<AIState>();
                if(state != null)
                {
                    connectedStates.Add(state);
                    actions.Add(new AIAction
                    {
                        name = state.name,
                        nextState = state
                    });
                }
            }
        }

        
    }
}
