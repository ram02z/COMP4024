using System.Collections;
using TMPro;
using UnityEngine;

// This class is responsible for managing the player's score and the game timer.
public class PointManager : MonoBehaviour
{
    public int score; // The player's current score.
    public TMP_Text scoreText; // The text object that displays the player's score.
    public TMP_Text timeText; // The text object that displays the remaining time.
    private WordManager _wordManager; // Reference to the WordManager component.
    private float _timeRemaining = 120f; // The remaining time in seconds.

    // This method is called at the start of the game.
    void Start()
    {
        score = 0;
        _wordManager = GameObject.Find("WordManager").GetComponent<WordManager>();
        Projectile.onEnemyHit.AddListener(UpdateScore);
        UpdateScoreUI();
        UpdateTimeUI();
        StartCoroutine(StartTimer());
    }

    // This coroutine starts the game timer.
    IEnumerator StartTimer()
    {
        while (_timeRemaining > 0)
        {
            yield return new WaitForSeconds(1);
            _timeRemaining--;
            UpdateTimeUI();
        }

        EndGame();
    }

    // This method ends the game.
    private void EndGame()
    {
        Application.Quit();
    }

    // This method updates the player's score.
    private void UpdateScore(GameObject enemy)
    {
        TextMeshPro enemyText = enemy.GetComponentInChildren<TextMeshPro>();
        if (enemyText.text.Equals(_wordManager.GetCurrentWord()))
        {
            _wordManager.ChangeWord();
            score += 10;
        }
        else
        {
            // Do not allow the score to go below 0
            if (score == 0) return;
            score -= 5;
        }
        UpdateScoreUI();
    }

    // This method updates the score text UI.
    void UpdateScoreUI()
    {
        scoreText.text = "SCORE: " + score;
    }

    // This method updates the time text UI.
    void UpdateTimeUI()
    {
        int minutes = Mathf.FloorToInt(_timeRemaining / 60);
        int seconds = Mathf.FloorToInt(_timeRemaining % 60);
        timeText.text = "TIME: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}