using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class holds the onClick logic for the home button
 */
public class HomeOnClick : MonoBehaviour
{
    /*
     * Load the TopicScene
     */
    public void TopicScreenCallback()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TopicScene");
    }
}
