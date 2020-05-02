using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace IL.Simulation
{   
    public class Teacher_SARSA : Teacher
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
                action == null ||
                action2 == null) return;

            if (student.Q.ContainsKey(state) &&
                student.Q[state].ContainsKey(action) &&
                student.Q.ContainsKey(state2) &&
                student.Q[state2].ContainsKey(action2))
            {
                var predict = student.Q[state][action];
                var target = reward + student.gamma * student.Q[state2][action2];
                var q = student.Q[state][action] + student.alpha * (target - predict);
                action.value = q;
                student.Q[state][action] = q;
            }
        }
    }
           
}