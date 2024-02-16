using UnityEngine;

/*
 A singleton class which will be used to maintain access to the 
vocabMap between the topics and game scenes
*/
public class ObjectManager : MonoBehaviour
{
    private static ObjectManager instance;
    public Vocabulary vocabMap;

    private void Awake()
    {
        // Ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // If vocabMap is not assigned, create a new instance of Vocabulary
            if (vocabMap == null)
            {
                vocabMap = new GameObject("Vocabulary").AddComponent<Vocabulary>();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
