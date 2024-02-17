using UnityEngine;

/*
 This class contains the onClick command for
topic the dynamically created topic buttons 
 */
public class TopicOnClick : MonoBehaviour
{
    /*
     This method obtains a reference to the Vocabulary singleton
    and then calls the TopicButtonClicked method with the buttons
    name as the topic paramater
     */
    public void OnButtonClick()
    {
        Debug.Log("on click ran");
        Vocabulary vocabulary = FindObjectOfType<Vocabulary>();

        if (vocabulary != null)
        {
            Debug.Log("Button clicked");
            vocabulary.TopicButtonClicked(gameObject.name);
        }
        else
        {
            Debug.Log("object manager is null");
        }
    }
}
