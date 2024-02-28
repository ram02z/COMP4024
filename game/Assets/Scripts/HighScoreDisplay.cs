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
            highScoreString += string.Format("{0}. Score: {1}, Date: {2}\n", counter, highScore.Item1, highScore.Item2);
            counter++;
        }

        highScoreText.text = highScoreString;
    }
}
