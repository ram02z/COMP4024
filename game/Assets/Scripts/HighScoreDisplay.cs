using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public GameObject scoreboardRowPrefab;
    public GameObject content;

    void Start()
    {
        UpdateHighScoreText();
    }

    void UpdateHighScoreText()
    {
        var highScores = FileUtil.ReadHighScoresFromFile();

        int counter = 1;
        foreach (var highScore in highScores)
        {
            var row = Instantiate(scoreboardRowPrefab, content.transform).GetComponent<ScoreboardRow>();
            row.rank.text = counter.ToString();
            row.score.text = highScore.Item1.ToString();
            row.date.text = highScore.Item2;
            counter++;
        }
    }
}
