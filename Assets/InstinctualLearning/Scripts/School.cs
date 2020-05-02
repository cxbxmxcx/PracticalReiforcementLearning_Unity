using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace IL.Simulation
{
    public class School : MonoBehaviour
    {
        public Teacher[] teachers;
        public ClassRoom[] classes;

        public bool debug;
                
        void Awake()
        {
            teachers = GetComponentsInChildren<Teacher>();
            classes = GetComponentsInChildren<ClassRoom>();
        }

        private void OnDrawGizmos()
        {
            if (debug)
            {
                var time = Time.realtimeSinceStartup;

                Handles.Label(transform.position, "Time {0}".Format(time.ToString("#.##")));
            }
        }
    }
}
