using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
The TopicManager class is used to populate the scroll view with
topic buttons, which correspond to the vocabulary CSV files.
 */
public class TopicManager : MonoBehaviour
{
    // Reference to the button prefab used for Button instatiation
    public GameObject buttonPrefab;
    // The content parameter of the scroll view on which to instatiate buttons
    public Transform contentTransform;
    public Button learnButton;
    public Button startGameButton;
    public Button scoreboardButton;
    private int _activeTopicButtons;

    /*
    This method obtains a list of CSV filenames from the FileUtil. For each
    name a new button is created, with the filename as the button text and
    button name.
     */
    public void Start()
    {
        // Clear the vocabulary map
        Vocabulary vocabulary = FindObjectOfType<Vocabulary>();
        vocabulary.vocabMap.Clear();

        // Set the active topic buttons to 0
        _activeTopicButtons = 0;
        
        // Add a listener to the start game button
        startGameButton.onClick.AddListener(StartGame);
        // Add a listener to the learn button
        learnButton.onClick.AddListener(Learn);
        // Add a listener to the scoreboard button
        scoreboardButton.onClick.AddListener(ScoreBoard);
        
        // Disable the learn and start game buttons
        learnButton.interactable = false;
        startGameButton.interactable = false;
        
        List<string> csvFiles = FileUtil.GetFileNames("CSV/");
        foreach(string topic in csvFiles)
        {
            CreateButton(topic);
        }
    }

    /*
     Creates a new button object with text and name [properties equal to
    buttonText.
     */
    public void CreateButton(string buttonText)
    {
        GameObject buttonObject = Instantiate(buttonPrefab, contentTransform);
        buttonObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(50f, 50f);
        UnityEngine.UI.Button buttonComponent = buttonObject.GetComponent<UnityEngine.UI.Button>();
        buttonComponent.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;
        buttonComponent.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;

        buttonObject.name = buttonText;
    }

    /*
     * Increments the activeTopicButtons counter
     */
    public void LogButtonActivation()
    {
        _activeTopicButtons++;
        ChangeButtonsActiveStatus();
    }

    /*
     * Decrements the activeTopicButtons counter
     */
    public void LogButtonDeactivation()
    {
        _activeTopicButtons--;
        ChangeButtonsActiveStatus();
    }

    /*
     *Activates or deactivates the learn button based on the output of AreButtonsActive()
     */
    private void ChangeButtonsActiveStatus()
    {
        learnButton.interactable = AreTopicButtonsActive();
        startGameButton.interactable = AreTopicButtonsActive();
    }

    /*
     * Returns true if there is at least 1 topic button selected
     */
    public bool AreTopicButtonsActive()
    {
        return _activeTopicButtons > 0;
    }

    // Callback for the start game button
    private void StartGame()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    // Callback for the learn button
    private void Learn()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LearnScene");
    }
    
    // Callback for the scoreboard button
    private void ScoreBoard()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ScoreBoardScene");
    }

}
