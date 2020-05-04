using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IL.Simulation
{
    [System.Serializable]
    public class AIState : MonoBehaviour
    {
        public string stateName;
        public List<AIAction> actions;       
        public float reward;
        public bool resetAgent;

        //used to set the states visible material
        public virtual void SetMaterial(Material mat)
        {
            
        }
                        
        public override int GetHashCode()
        {
            return stateName.GetHashCode();
        }
    }
}
