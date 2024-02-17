using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Debug object start ran");
        ObjectManager objectManager = FindObjectOfType<ObjectManager>();

        if (objectManager != null)
        {
            Debug.Log("ObjectManager exists");
            objectManager.PrintVocabToDebug();
        }
        else
        {
            Debug.Log("ObjectManager does not exist");

        }
    }

}
