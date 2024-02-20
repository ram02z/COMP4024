using System.Collections;
using TMPro;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public int score;
    public TMP_Text scoreText;
    public TMP_Text timeText;
    public WordManager wordManager;
    private float _timeRemaining = 120f;
    
    void Start()
    {
        score = 0;
        wordManager = GameObject.Find("WordManager").GetComponent<WordManager>();
        UpdateScoreUI();
        UpdateTimeUI();
        StartCoroutine(StartTimer());
    }
    
    IEnumerator StartTimer()
    {
        while (_timeRemaining > 0)
        {
            yield return new WaitForSeconds(1);
            _timeRemaining--;
            UpdateTimeUI();
        }
        
        Application.Quit();
    }
    
    public void UpdateScore(GameObject enemy)
    {
        TextMeshPro enemyText = enemy.GetComponentInChildren<TextMeshPro>();
        if (enemyText.text.Equals(wordManager.GetCurrentWord()))
        {
            wordManager.ChangeWord();
            score += 10;
        }
        else
        {
            if (score == 0) return;
            score -= 5;
        }
        UpdateScoreUI();
    }
    
    void UpdateScoreUI()
    {
        scoreText.text = "SCORE: " + score;
    }
    
    void UpdateTimeUI()
    {
        int minutes = Mathf.FloorToInt(_timeRemaining / 60);
        int seconds = Mathf.FloorToInt(_timeRemaining % 60);
        timeText.text = "TIME: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
