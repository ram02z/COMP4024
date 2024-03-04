using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeOnClick : MonoBehaviour
{
    public void TopicScreenCallback()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TopicScene");
    }
}
