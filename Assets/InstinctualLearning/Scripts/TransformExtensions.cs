using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IL.Simulation
{
    public static class TransformExtensions
    {
        public static void Copy(this Transform original, Transform copy)
        {
            original.position = copy.position;
            original.rotation = copy.rotation;
            original.localScale = copy.localScale;
        }
    }
}
