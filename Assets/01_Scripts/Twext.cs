using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twext : MonoBehaviour
{
    [SerializeField] private bool stopTime;
    
    void Start()
    {
        
    }

    float startTime;
    private float newTime;
    void Update()
    {
        float time = 0;
        if(!stopTime)
        {
            time = Time.time - startTime;
        }
        else
        {
            newTime = Time.time - time;
        }
        Debug.Log(time + newTime);
            
    }

    void DoTimerOffset() { startTime = Time.time; }
}
