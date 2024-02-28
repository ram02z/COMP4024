using System;
using UnityEngine.Events;
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
    public IEnumerator StartTimer()
    {
        while (timeRemaining > 0)
        {
            yield return new WaitForSeconds(1);
            timeRemaining--;
            onTimeChanged.Invoke(timeRemaining);
        }
        EndGame();
    }
    
    // This method ends the game and goes to the Topic Scene.
    private void EndGame()
    {
        // Get the current highscore
        int highscore = GameObject.Find("PointManager").GetComponent<PointManager>().score;

        // Get the current date and time
        string dateTime = DateTime.Now.ToString();

        // Define the path where the highscore will be saved
        string path = Application.persistentDataPath + "/highscore.txt";

        // Save the highscore and the current date and time to the file
        string dataToSave = highscore + ", " + dateTime + Environment.NewLine;
        System.IO.File.AppendAllText(path, dataToSave);
        // TODO: Go to game over scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("TopicScene");
    }
}