using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static bool isTimerActive;
    public static float currentTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerActive == true)
        {
            currentTime += Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        Debug.Log(time);
    }

    public void StartTimer()
    {
        isTimerActive = true;
    }
    public void StopTimer()
    {
        isTimerActive = false;
    }
}
