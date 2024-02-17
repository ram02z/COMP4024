using System;
using System.Collections.Generic;
using UnityEngine;

/*
The ObjectManager is a singleton, which maintains a reference
to the Vocabulary object so that it can be accessed
when the game play scene is loaded. It contains a public method
TopicButtonClicked, which is called by topic Button objects.
Helper methods then handle the adding or removing of vocabulary from the 
Vocabulary object based on the topic button clicks
*/
public class ObjectManager : MonoBehaviour
{
    private static ObjectManager instance;
    public Vocabulary vocabMap;

    /*
     Ensures that one and only one instance of the ObjectManager
    exists and prevents this from being destroyed between scene changes
     */
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

    /*
     When given a topic string this method will call upon
    the FileUtil to read the corresponfing topic CSV file and
    then add the this entry to the Vocabulary object.
     */
    private void AddTopicToVocabMap(string topic)
    {
        string csvFilepath = "Assets/CSV/" + topic;
        Dictionary<string, string> csvValues = FileUtil.ReadCSVFile(csvFilepath);
        vocabMap.AddTopicVocab(topic, csvValues);
    }

    /*
     This method is called by topic Buttons. If the topic already
    exists within the Vocabulary object then it is removed and false is returned.
    If it does not exist then the topic CSV file is loaded, and the vocabulary
    is added to the Vocabulary object then true is returned.

    */
    public Boolean TopicButtonClicked(string topic)
    {
        if (!vocabMap.IsTopicInVocabulary(topic))
        {
            AddTopicToVocabMap(topic);
            vocabMap.PrintToDebug();
            return true;
        }
        else
        {
            vocabMap.RemoveTopicVocab(topic);
            vocabMap.PrintToDebug();
            return false;
        }

    }
}
