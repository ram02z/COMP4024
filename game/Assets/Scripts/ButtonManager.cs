using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
The TopicManager class is used to populate the scroll view with
topic buttons, which correspond to the vocabulary CSV files.
 */
public class ButtonManager : MonoBehaviour
{
    // Reference to the button prefab used for Button instatiation
    public GameObject buttonPrefab;
    // The content parameter of the scroll view on which to instatiate buttons
    public Transform contentTransform;
    public Button learnButton;
    public Button startGameButton;
    private int _activeTopicButtons = 0;

    /*
    This method obtains a list of CSV filenames from the FileUtil. For each
    name a new button is created, with the filename as the button text and
    button name.
     */
    public void Start()
    {
        // Add a listener to the start game button
        startGameButton.onClick.AddListener(StartGame);
        // Add a listener to the learn button
        learnButton.onClick.AddListener(Learn);
        
        // Disable the learn and start game buttons
        learnButton.interactable = false;
        startGameButton.interactable = false;
        
        List<string> csvFiles = FileUtil.GetFileNames("Assets/CSV");
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
        buttonComponent.GetComponentInChildren<Text>().text = buttonText;
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

}
