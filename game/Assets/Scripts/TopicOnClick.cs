using System;
using UnityEngine;

/*
 This class contains the onClick command for
 the dynamically created topic buttons
 */
public class TopicOnClick : MonoBehaviour
{

    private Color selectedColour = Color.red;
    private Color unselectedColour;

    /*
     Obtains the colour of the button object at instatiation
     */
    private void Start()
    {
        unselectedColour = GetComponent<UnityEngine.UI.Image>().color;
    }

    /*
     Updates the buttons colour to selectedColour is passed true
    or changes the buttons colour to the unselectedColour when passed false.
     */
    private void SwitchColour(Boolean selected)
    {
        if(selected)
        {
            GetComponent<UnityEngine.UI.Image>().color = selectedColour;
        }
        else
        {
            GetComponent<UnityEngine.UI.Image>().color = unselectedColour;
        }
    }

    /*
     This method obtains a reference to the Vocabulary singleton
    and then calls the TopicButtonClicked method with the buttons
    name as the topic paramater. The TopicButtonClicked method returns
    a boolean representing whether the buttons associated vocabulary
    has been added or removed. This is then used to change button colour.
     */
    public void OnButtonClick()
    {
        Debug.Log("on click ran");
        Vocabulary vocabulary = FindObjectOfType<Vocabulary>();


        if (vocabulary != null)
        {
            Debug.Log("Button clicked");
            SwitchColour(vocabulary.TopicButtonClicked(gameObject.name));
        }
        else
        {
            Debug.Log("object manager is null");
        }
    }
}
