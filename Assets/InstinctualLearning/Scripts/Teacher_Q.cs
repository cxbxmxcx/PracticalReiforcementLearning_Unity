using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace IL.Simulation
{    
    public class Teacher_Q : Teacher
    {
        public override void Teach(StudentBrain student,
            AIState state,
            AIState state2,
            float reward,
            AIAction action,
            AIAction action2)
        {
            if (student.assigned == false ||
                state == null ||
                action == null) return;

            if (student.Q.ContainsKey(state) && student.Q[state].ContainsKey(action))
            {
                var predict = student.Q[state][action];
                var target = reward + student.gamma * student.Q[state2].Max(a => a.Value); //[action2];
                var q = student.Q[state][action] + student.alpha * (target - predict);
                action.value = q;
                student.Q[state][action] = q;
            }
        }       
    }
           
}