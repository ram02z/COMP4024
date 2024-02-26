using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugObject : MonoBehaviour
{
    // Used to check that the Vocabulary persists between topic and game scenes
    void Start()
    {
        Debug.Log("Debug object start ran");
        Vocabulary vocabulary = FindObjectOfType<Vocabulary>();

        if (vocabulary != null)
        {
            Debug.Log("Vocabulary exists");
        }
        else
        {
            Debug.Log("Vocabulary does not exist");

        }
    }

}
