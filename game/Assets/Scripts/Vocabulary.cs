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
    private Dictionary<string, string> vocabMap;
    private Dictionary<string, string> topicMap;
    private static Vocabulary instance;

    /*
     Instatiates a Dictionary object for the vocabMap member variable.
    This is primarily required for testing as access to the awake method
    is restricted
     */
    public void SetupVocabMap()
    {
        vocabMap = new Dictionary<string, string>();
    }

    /*
    Ensures that one and only one instance of the Vocabulary
   exists and prevents this from being destroyed between scene changes
    */
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            if (vocabMap == null)
            {
                vocabMap = new Dictionary<string, string>();
            }

            if (topicMap == null)
            {
                topicMap = new Dictionary<string, string>();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /*
     * Adds the given topic and its associated vocabulary to the
     * vocabMap. If the topic already exists then the vocabulary
     * is updated with the new values.
     */
    public void AddTopicVocab(string topic, Dictionary<string, string> vocabulary)
    {
        foreach (var (frenchWord, englishWord) in vocabulary)
        {
            vocabMap.TryAdd(frenchWord, englishWord);
            topicMap.TryAdd(frenchWord, topic);
        }
    }

    /*
     Checks if the given topic string exists as a key in the
    vocabMap. If it does then this entry will be removed from the
    vocabMap
     */
    public void RemoveTopicVocab(string topic)
    {
        List<string> wordsToRemove = new List<string>();

        foreach (var entry in topicMap)
        {
            if (entry.Value == topic)
            {
                wordsToRemove.Add(entry.Key);
            }
        }

        foreach (var word in wordsToRemove)
        {
            vocabMap.Remove(word);
            topicMap.Remove(word);
        }
    }

    /*
     Checks if the given topic string exists as a value in the topicMap.
    Returns true if the value is present else false
    */
    public bool IsTopicInVocabulary(string topic)
    {
        return topicMap.ContainsValue(topic);
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
     * Returns dictionary of all french words and their english translations
     */
    public Dictionary<string, string> GetVocabMap()
    {
        return vocabMap;
    }
    
    /*
     Returns the english translation of the given french word
    */
    public string GetEnglishTranslation(string frenchWord)
    {
        if (vocabMap.TryGetValue(frenchWord, out var englishWord))
        {
            return englishWord;
        }
        Debug.LogError("Word not found in Vocabulary");
        return null;
    }

    /*
     This method is called by topic Buttons. If the topic already
    exists within the Vocabulary object then it is removed and false is returned.
    If it does not exist then the topic CSV file is loaded, and the vocabulary
    is added then true is returned.

    */
    public bool TopicButtonClicked(string topic)
    {
        if (!IsTopicInVocabulary(topic))
        {
            AddTopicToVocabMap(topic);
            return true;
        }
        else
        {
            RemoveTopicVocab(topic);
            return false;
        }

    }

}
