using System.Collections.Generic;
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

    /*
    This method obtains a list of CSV filenames from the FileUtil. For each
    name a new button is created, with the filename as the button text and
    button name.
     */
    void Start()
    {
        List<string> csvFiles = FileUtil.GetFileNames();
        
        foreach(string topic in csvFiles)
        {
            CreateButton(topic);
        }
    }

    /*
     Creates a new button object with text and name [properties equal to
    buttonText.
     */
    void CreateButton(string buttonText)
    {
        GameObject buttonObject = Instantiate(buttonPrefab, contentTransform);
        buttonObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(50f, 50f);
        UnityEngine.UI.Button buttonComponent = buttonObject.GetComponent<UnityEngine.UI.Button>();
        buttonComponent.GetComponentInChildren<Text>().text = buttonText;
        buttonObject.name = buttonText;
    }
}
