using System;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public TMP_Text wordText; // The text object that displays the current word.
    public TMP_Text scoreText; // The text object that displays the player's score.
    public TMP_Text timeText; // The text object that displays the remaining time.

    private void Awake()
    {
        GameObject timeManagerObject = GameObject.Find("TimeManager");
        TimeManager timeManager = timeManagerObject.GetComponent<TimeManager>();
        timeManager.onTimeChanged.AddListener(UpdateTimeUI);
    }

    void Start()
    {
        GameObject pointManagerObject = GameObject.Find("PointManager");
        PointManager pointManager = pointManagerObject.GetComponent<PointManager>();
        pointManager.onScoreChanged.AddListener(UpdateScoreUI);

        GameObject wordManagerObject = GameObject.Find("WordManager");
        WordManager wordManager = wordManagerObject.GetComponent<WordManager>();
        wordManager.onWordChanged.AddListener(UpdateWordUI);
    }
    
    // This method updates the wordText object with the given word.
    public void UpdateWordUI(string word)
    {
        wordText.text = word;
    }
    
    public void UpdateScoreUI(int score)
    {
        scoreText.text = "SCORE: " + score;
    }
    
    public void UpdateTimeUI(float timeRemaining)
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timeText.text = "TIME: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
