using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace IL.Simulation
{   
    public class Teacher : MonoBehaviour
    {   
        public virtual void Teach(StudentBrain student, 
            AIState state, 
            AIState state2, 
            float reward, 
            AIAction action, 
            AIAction action2)
        {
            return;
        }
    }

    [System.Serializable]
    public class AIAction
    {
        public string name;
        public AIState nextState;
        public float value;
        public override int GetHashCode()
        {
            if (string.IsNullOrEmpty(name))
            {
                return base.GetHashCode();
            }
            else
            {
                return name.GetHashCode();
            }
        }
    }    
}