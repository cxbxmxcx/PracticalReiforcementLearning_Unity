using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace IL.Simulation
{
    public class StudentBrain : MonoBehaviour
    {
        [Header("Maximum number of decisions for brain to make")]
        public int maxExperience = 100;
        public int currentExperience;

        [Header("Discount rate - gamma")]
        [SerializeField]
        private float _gamma;
        public float gamma
        {
            get
            {
                return _gamma;
            }
            set
            {
                _gamma = value;
            }
        }
        public AnimationCurve discountCurve;

        [Header("Exploration/Exploitation - epsilon")]
        [SerializeField]
        private float _epsilon;
        public float epsilon
        {
            get
            {
                return _epsilon;
            }
            set
            {
                _epsilon = value;
            }
        }
        public AnimationCurve explorationCurve;

        [Header("Learning rate - alpha")]
        [SerializeField]
        private float _alpha;
        public float alpha
        {
            get
            {
                return _alpha;
            }
            set
            {
                _alpha = value;
            }
        }
        public AnimationCurve learningRateCurve;

        public Teacher teacher;
        public bool assigned;

        [Header("DEBUG - parameters for debugging")]
        public bool debug;

        public Dictionary<AIState, Dictionary<AIAction, float>> Q;
        private Gradient gradient;

        private void Start()
        {
            gradient = new Gradient();

            var colorkeys = new GradientColorKey[2];
            var alphakeys = new GradientAlphaKey[2];

            colorkeys[0].color = Color.red;
            colorkeys[0].time = 0f;
            colorkeys[1].color = Color.green;
            colorkeys[1].time = 1f;

            alphakeys[0].alpha = 1f;
            alphakeys[0].time = 0f;
            alphakeys[1].alpha = 1f;
            alphakeys[1].time = 1f;

            gradient.SetKeys(colorkeys, alphakeys);
        }
        
        private void OnDrawGizmos()
        {
            if (debug && Q != null)
            {
                foreach (var state in Q.Keys)
                {
                    var start = state.transform.position;
                    Handles.Label(start + Vector3.up, state.stateName);
                    foreach (var act in Q[state])
                    {
                        var look = (act.Key.nextState.transform.position - start).normalized + start;
                        var color = gradient.Evaluate(act.Value);
                        Handles.Label(look, act.Value.ToString("#.##"));
                    }                    
                }
            }
        }

        public AIAction ActGreedy(AIState state)
        {
            if (Q == null || Q.Keys.Count < 1) return null;
            return Q[state].Aggregate((a1, a2) => a1.Value > a2.Value ? a1 : a2).Key;
        }

        public AIAction Act(AIState state)
        {
            if (Q == null || Q.Keys.Count < 1) return null;
            var re = Random.Range(0f, 1f);
            if (re > epsilon)
            {
                return Q[state].Aggregate((a1, a2) => a1.Value > a2.Value ? a1 : a2).Key;
            }
            else
            {                
                var keys = Q[state].Keys.ToList();
                var r = Random.Range(0, keys.Count);
                return keys[r];
            }
        }

        //normalize all the values in the brain
        public virtual void NormalizeBrain()
        {
            if (Q == null || Q.Keys.Count < 1) return;

            var maxA = float.MinValue;
            var minA = float.MaxValue;
            foreach (var state in Q.Keys)
            {
                maxA = Mathf.Max(maxA, Q[state].Max(_ => _.Value));
                minA = Mathf.Min(minA, Q[state].Min(_ => _.Value));
            }

            var rng = maxA - minA;            
            foreach (var state in Q.Keys)
            {
                var acts = Q[state].Keys.ToList();
                foreach(var act in acts)
                {
                    var val = (Q[state][act] - minA) / rng;
                    Q[state][act] = val;
                    act.value = val;
                }                
            }
        }

        public virtual void ResetBrain()
        {
            foreach (var state in Q.Keys)
            {
                var acts = Q[state].Keys.ToList();
                foreach (var act in acts)
                {
                    var val = 1;
                    Q[state][act] = val;
                    act.value = val;
                }
            }
        }

        public virtual void Learn(AIState state,
            AIState state2,
            float reward,
            AIAction action,
            AIAction action2)
        {
            if (teacher != null)
            {
                teacher.Teach(this, state, state2, reward, action, action2);
                UpdateExperience();
            }
        }

        private void UpdateExperience()
        {
            currentExperience += 1;

            var time = Mathf.Clamp01((float)currentExperience / (float)maxExperience);
            gamma = discountCurve.Evaluate(time);
            epsilon = explorationCurve.Evaluate(time);
            alpha = learningRateCurve.Evaluate(time);
        }

        public void InitBrainStates(AIState[] states)
        {
            Q = new Dictionary<AIState, Dictionary<AIAction, float>>();
            foreach (var state in states)
            {
                var actTable = new Dictionary<AIAction, float>();
                foreach (var action in state.actions)
                {
                    //initialize to random (0.0, 1.0]
                    actTable.Add(action, 1.0f);
                }
                Q.Add(state, actTable);
            }

            assigned = true;
        }

        
    }

    
}
