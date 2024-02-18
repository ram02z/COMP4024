using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    public int score;
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText.text = "SCORE: " + score;
    }
    
    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "SCORE: " + score;
        Debug.Log("Score: " + score);
    }
}
