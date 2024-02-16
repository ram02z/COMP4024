using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 This class will be responsible for dynamically generating topic buttons
 */
public class ButtonManager : MonoBehaviour
{
    // Reference to the button prefab (Button) used to give a template for
    //.Button generation
    public GameObject buttonPrefab;
    // The canvas on which to generate the dynamic button objects
    public Canvas canvas;

    /*
    The start method will dynamically generate buttons
    for each topic using the filenames present in the CSV folder
     */
    void Start()
    {
        // Performing some debugging to check that the FileUtil REadCSVFile method works
        // Correctly
        string filePath = "Assets/CSV/Access";
        Dictionary<string, string> csvData = FileUtil.ReadCSVFile(filePath);

        // Instantiate a new Button object
        CreateButton("new button");
    }

    /*
     Generates a new Button with with the given text displayed
     */
    void CreateButton(string buttonText)
    {
        GameObject buttonObject = Instantiate(buttonPrefab, canvas.transform);
        buttonObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(50f, 50f);
        Button buttonComponent = buttonObject.GetComponent<Button>();
        buttonComponent.GetComponentInChildren<Text>().text = buttonText;
        buttonComponent.onClick.AddListener(() => ButtonClick(buttonText));
    }

    /*
     Place holder for the buttons on click method
     */
    void ButtonClick(string buttonText)
    {
        Debug.Log("Button Clicked: " + buttonText);
    }
}
