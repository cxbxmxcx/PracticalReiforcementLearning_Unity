using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions 
{
    public static Vector3 Copy(this Vector3 copy)
    {
        return new Vector3(copy.x, copy.y, copy.z);
    }
}
