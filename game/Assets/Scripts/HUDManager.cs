using TMPro;
using UnityEngine;

// This class is responsible for managing the Heads-Up Display (HUD) in the game.
public class HUDManager : MonoBehaviour
{
    // These are the text objects that display the current word, player's score, and remaining time.
    public TMP_Text wordText;
    public TMP_Text scoreText;
    public TMP_Text timeText;

    // Awake is called when the script instance is being loaded.
    // Here we find the TimeManager object and get its TimeManager component.
    // Then, we subscribe the UpdateTimeUI method to the onTimeChanged event.
    private void Awake()
    {
        GameObject timeManagerObject = GameObject.Find("TimeManager");
        TimeManager timeManager = timeManagerObject.GetComponent<TimeManager>();
        timeManager.onTimeChanged.AddListener(UpdateTimeUI);
    }

    // Start is called before the first frame update.
    // Here we find the PointManager and WordManager objects, get their respective components,
    // and subscribe the UpdateScoreUI and UpdateWordUI methods to the onScoreChanged and onWordChanged events.
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

    // This method updates the scoreText object with the given score.
    public void UpdateScoreUI(int score)
    {
        scoreText.text = "SCORE: " + score;
    }

    // This method updates the timeText object with the given time remaining.
    // The time is displayed in the format "TIME: MM:SS".
    public void UpdateTimeUI(float timeRemaining)
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timeText.text = "TIME: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}