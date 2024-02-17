using System;
using System.Collections.Generic;
using UnityEngine;


/*
The Vocabulary object represnts a Dictionary of topics and their
associated french/english translation pairs. It contains methods
used for adding and removing topics, checking if a topic exists
in the dictionary and printing itself to the debug log.
 */
public class Vocabulary : MonoBehaviour
{
    public Dictionary<string, Dictionary<string, string>> vocabMap;

    /*
     Instatiate a dictionary object named vocabMap: Dictionary<string, Dictionary<string, string>>
    where vocabMap contains <topic name, Dictonary<french word, english word>>
    */
    public Vocabulary()
    {
        vocabMap = new Dictionary<string, Dictionary<string, string>>();
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
        IsTopicInVocabulary(topic);
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
        Debug.Log("IsTopicInVocabulary: " + vocabMap.ContainsKey(topic));
        return vocabMap.ContainsKey(topic);
    }

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
