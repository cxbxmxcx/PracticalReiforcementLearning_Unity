using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IL.Simulation
{
    public class SchoolMaster : MonoBehaviour
    {
        public StudentAgent[] students;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(DelayStart());
        }

        private IEnumerator DelayStart()
        {
            //wait until other scene objects are all initialized
            yield return new WaitForSeconds(1);

            //locate all the students
            students = GetComponentsInChildren<StudentAgent>();

            foreach(var student in students)
            {
                if(student.brain != null &&
                    student.brain.assigned == false &&
                    student.classRoom.states != null &&
                    student.classRoom.states.Length > 0)
                {
                    student.brain.InitBrainStates(student.classRoom.states);
                }
                student.classRoom.ResetAgent(student);
            }
        }

        
    }
}
