using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringExtensions 
{
    public static string Format(this string format, params string[] ps)
    {
        return string.Format(format, ps);
    }
}
