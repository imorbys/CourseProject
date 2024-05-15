using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float startTime;
    private bool running;
    private float currentTime;
    public void Start()
    {
        startTime = Time.time;
        running = true;
    }
    public void Stop()
    {
        running = false;
    }
    public string GetFormattedTime()
    {
        if (!running)
            return "";

        float currentTime = Time.time - startTime;
        int minutes = (int)(currentTime / 60);
        int seconds = (int)(currentTime % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
