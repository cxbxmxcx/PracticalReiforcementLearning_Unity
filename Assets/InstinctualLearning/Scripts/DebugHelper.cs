using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IL.Simulation
{
    public static class DebugHelper
    {
        public static void Log(Color color, string message)
        {
            if (Debug.isDebugBuild)
            {
                ColorUtility.ToHtmlStringRGB(color);
                Debug.Log("<color=#{0}>{1}</color>".Format(ColorUtility.ToHtmlStringRGB(color), message));
            }
        }
    }
}
