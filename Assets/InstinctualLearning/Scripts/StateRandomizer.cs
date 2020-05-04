using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IL.Simulation
{
    public class StateRandomizer : MonoBehaviour
    {
        public ClassRoom classRoom;
        public SchoolMaster schoolMaster;

        public Material positiveRewards;
        public Material negativeRewards;
        public Material neutralRewards;

        public int positiveStates = 1;
        public int negativeStates = 3;

        private void Start()
        {
            if (positiveRewards == null ||
                negativeRewards == null ||
                neutralRewards == null)
            {
                Debug.LogError("StateRandomizer needs materials set");
            }

            if (classRoom == null || schoolMaster == null)
            {
                Debug.LogError("StateRandomizer needs ClassRoom and SchoolMaters set");
            }
        }

        public void RandomizeStates()
        {
            if (classRoom == null || classRoom.states.Length < 1) return;
            var cnt = classRoom.states.Length;
            var pos = 0f;
            var neg = 0f;
            for(int i = 0; i < classRoom.states.Length; i++)
            {
                var state = classRoom.states[i];
                if(state == classRoom.startState) continue;

                var posP = (positiveStates - pos) / (float)(cnt - i);
                var negP = (negativeStates - neg) / (float)(cnt - i);

                if(Random.Range(0.0f, 1.0f) < posP)
                {
                    pos += 1;
                    state.reward = 1.0f;
                    state.resetAgent = true;
                    state.SetMaterial(positiveRewards);
                    continue;
                }else if (Random.Range(0.0f, 1.0f) < negP)
                {
                    neg += 1;
                    state.reward = -1.0f;
                    state.resetAgent = true;
                    state.SetMaterial(negativeRewards);
                    continue;
                }
                else
                {
                    state.reward = 0.0f;
                    state.resetAgent = false;
                    state.SetMaterial(neutralRewards);
                }
            }

            //collect all the brains and reset the experience and renormalize
            List<StudentBrain> brains = new List<StudentBrain>();
            foreach(var student in schoolMaster.students)
            {
                if (student.classRoom != classRoom) continue;
                classRoom.ResetAgent(student);
                if(brains.Contains(student.brain)== false)
                {
                    brains.Add(student.brain);
                }
            }

            foreach(var brain in brains)
            {
                brain.currentExperience = 0;
                brain.ResetBrain();
            }
        }
    }
}
