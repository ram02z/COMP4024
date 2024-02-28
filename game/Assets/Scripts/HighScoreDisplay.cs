using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        UpdateHighScoreText();
    }

    void UpdateHighScoreText()
    {
        var highScores = FileUtil.ReadHighScoresFromFile();

        string highScoreString = "";
        int counter = 1;
        foreach (var highScore in highScores)
        {
            highScoreString += $"{counter}. Score: {highScore.Item1}, Date: {highScore.Item2}\n";
            counter++;
        }

        highScoreText.text = highScoreString;
    }
}
