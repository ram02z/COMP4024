using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
This class will represent a vocabulary object, which contains a vocabMap variable.
This vocabMap is a dictionary of <vocab topic from csv file (string), list of french/english translations (List<string>)>
 */
public class Vocabulary : MonoBehaviour
{
    public Dictionary<string, List<string>> vocabMap;

    /*
     Instatiate a dictionary object to hold the vocabulary strings
    */
    public Vocabulary()
    {
        vocabMap = new Dictionary<string, List<string>>();
    }

    /*
     Adds an entry to the vocabMap
    */
    public void AddTopicVocab(string topic, List<string> vocabulary)
    {
        vocabMap.Add(topic, vocabulary);
    }

    /*
     Returns the dictionary of topics and associated vocabulary
    */
    public Dictionary<string, List<string>> GetVocabMap()
    {
        return vocabMap;
    }
}
