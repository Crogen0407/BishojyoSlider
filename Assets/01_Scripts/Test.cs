using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        Func<int, int[]> func;
        func = (a) =>
        {
            return new int[a];
        };
    }
}
