using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreChangedEvent : UnityEvent<int> { }

// This class is responsible for managing the player's score and the game timer.
public class PointManager : MonoBehaviour
{
    public int score; // The player's current score.
    private WordManager _wordManager; // Reference to the WordManager component.
    public ScoreChangedEvent onScoreChanged = new(); // Event that is invoked when the score changes.

    // This method is called at the start of the game.
    void Start()
    {
        score = 0;
        _wordManager = GameObject.Find("WordManager").GetComponent<WordManager>();
        Projectile.OnEnemyHit.AddListener(UpdateScore);
        onScoreChanged.Invoke(score);
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
        onScoreChanged.Invoke(score);
    }
}