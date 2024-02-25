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

    private int activeTopicButtons = 0;

    /*
    This method obtains a list of CSV filenames from the FileUtil. For each
    name a new button is created, with the filename as the button text and
    button name.
     */
    public void Start()
    {
        learnButton.gameObject.SetActive(false);
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
        activeTopicButtons++;
        ChangeLearnButtonActiveStatus();
    }

    /*
     * Decrements the activeTopicButtons counter
     */
    public void LogButtonDeactivation()
    {
        activeTopicButtons--;
        ChangeLearnButtonActiveStatus();
    }

    /*
     *Activates or deactivates the learn button based on the output of AreButtonsActive()
     */
    private void ChangeLearnButtonActiveStatus()
    {
        if (AreButtonsActive())
        {
            learnButton.gameObject.SetActive(true);
        }
        else
        {
            learnButton.gameObject.SetActive(false);
        }
    }

    /*
     * Returns true if there is at least 1 topic button selected
     */
    public Boolean AreButtonsActive()
    {
        return activeTopicButtons > 0;
    }
}
