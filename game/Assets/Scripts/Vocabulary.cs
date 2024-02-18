using System;
using System.Collections.Generic;
using UnityEngine;


/*
The Vocabulary object represnts a Dictionary of topics and their
associated french/english translation pairs. It contains methods
used for adding and removing topics, checking if a topic exists
in the dictionary and printing itself to the debug log. Vocabulary
is a singleton class so that it can persist between scenes
 */
public class Vocabulary : MonoBehaviour
{
    public Dictionary<string, Dictionary<string, string>> vocabMap;
    private static Vocabulary instance;

    /*
     Instatiates a Dictionary object for the vocabMap member variable.
    This is primarily required for testing as access to the awake method
    is restricted
     */
    public void SetupVocabMap()
    {
        vocabMap = new Dictionary<string, Dictionary<string, string>>();
    }

    /*
    Ensures that one and only one instance of the Vocabulary
   exists and prevents this from being destroyed between scene changes
    */
    private void Awake()
    {
        // Ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Create the vocabMap
            if (vocabMap == null)
            {
                SetupVocabMap();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /*
     Checks if the provided topic exists in the vocabMap. If it does not
    then a new entry will be added with the topic string as the key.
    */
    public void AddTopicVocab(string topic, Dictionary<string, string> vocabulary)
    {
        Debug.Log("AddTopicVocab: " + topic);
        if (!IsTopicInVocabulary(topic))
        {
            vocabMap.Add(topic, vocabulary);
        }
    }

    /*
     Checks if the given topic string exists as a key in the
    vocabMap. If it does then this entry will be removed from the
    vocabMap
     */
    public void RemoveTopicVocab(string topic)
    {
        Debug.Log("RemoveTopicVocab: " + topic);
        if (IsTopicInVocabulary(topic))
        {
            Debug.Log("removing......");
            vocabMap.Remove(topic);
        }
    }

    /*
     Returns the vocabMap dictionary: <string, Dictionary<string, string>>
    where the vocabMap contains <topic name, Dictionary<french word, english word>> 
    */
    public Dictionary<string, Dictionary<string, string>> GetVocabMap()
    {
        return vocabMap;
    }

    /*
     Checks if the given topic string exists as a key in the vocabMap.
    Returns true if the key is present else false
    */
    public Boolean IsTopicInVocabulary(string topic)
    {
        Debug.Log("IsTopicInVocabulary: called");
        Debug.Log("IsTopicInVocabulary: " + vocabMap.ContainsKey(topic));
        return vocabMap.ContainsKey(topic);
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
        AddTopicVocab(topic, csvValues);
    }

    /*
     This method is called by topic Buttons. If the topic already
    exists within the Vocabulary object then it is removed and false is returned.
    If it does not exist then the topic CSV file is loaded, and the vocabulary
    is added then true is returned.

    */
    public Boolean TopicButtonClicked(string topic)
    {
        if (!IsTopicInVocabulary(topic))
        {
            AddTopicToVocabMap(topic);
            PrintToDebug();
            return true;
        }
        else
        {
            RemoveTopicVocab(topic);
            PrintToDebug();
            return false;
        }

    }

    /*
     Prints the contents of the vocab dictionary to the debug log
     */
    public void PrintToDebug()
    {
        if (vocabMap != null)
        {
            foreach (var topicEntry in vocabMap)
            {
                string topic = topicEntry.Key;
                Dictionary<string, string> topicVocab = topicEntry.Value;

                Debug.Log($"Topic: {topic}");

                foreach (var vocabEntry in topicVocab)
                {
                    string key = vocabEntry.Key;
                    string value = vocabEntry.Value;

                    Debug.Log($"  {key}: {value}");
                }
            }
        }
        else
        {
            Debug.LogWarning("vocabMap is null");
        }
    }

}
