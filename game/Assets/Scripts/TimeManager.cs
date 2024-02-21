﻿using UnityEngine.Events;
using System.Collections;
using UnityEngine;

public class TimeChangedEvent : UnityEvent<float> { }

public class TimeManager : MonoBehaviour
{
    public float timeRemaining = 120f; // The remaining time in seconds.
    public TimeChangedEvent onTimeChanged = new(); // Event that is invoked when the time is changed.

    // This method is called at the start of the game.
    void Start()
    {
        onTimeChanged.Invoke(timeRemaining);
        StartCoroutine(StartTimer());
    }

    // This coroutine starts the game timer.
    IEnumerator StartTimer()
    {
        while (timeRemaining > 0)
        {
            yield return new WaitForSeconds(1);
            timeRemaining--;
            onTimeChanged.Invoke(timeRemaining);
        }
        EndGame();
    }
    
    // This method ends the game.
    private void EndGame()
    {
        Application.Quit();
    }
}