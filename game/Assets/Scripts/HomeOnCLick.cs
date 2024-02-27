using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeOnCLick : MonoBehaviour
{
    public void TopicScreenCallback()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TopicScene");
    }
}
