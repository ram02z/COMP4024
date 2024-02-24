using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RevisionTextController : MonoBehaviour
{
    public TextMeshProUGUI englishText;
    public TextMeshProUGUI frenchText;
    private int vocabularyTopicIndex = 0;
    private int vocabularyWordIndex = -1;
    private Dictionary<string, Dictionary<string, string>> vocabularyDict;
    private List<string> topics;

    public void Start()
    {
        Vocabulary vocabulary = FindObjectOfType<Vocabulary>();
        vocabularyDict = vocabulary.GetVocabMap();
        topics = new List<string>(vocabularyDict.Keys);

        NextCard();
    }


    private void UpdateText(string english, string french)
    {
        englishText.text = english;
        frenchText.text = french;
    }

    private List<string> GetNextWordPair()
    {
        // Check if the current index is the last entry in the entire vocab dictionary
        int maxIndexOfFinalTopic = vocabularyDict[topics[topics.Count - 1]].Count - 1;
        if ((vocabularyTopicIndex == topics.Count - 1) && 
            (vocabularyWordIndex == maxIndexOfFinalTopic))
        {
            Debug.Log("GetNextWordPair: null");
            return null;
        }

        // Check if the current index is the last entry in the current vocab topic
        int maxIndexOfCurrentTopic = vocabularyDict[topics[vocabularyTopicIndex]].Count - 1;
        if ((vocabularyWordIndex == maxIndexOfCurrentTopic) &&
            (vocabularyTopicIndex < topics.Count - 1))
        {
            Debug.Log("GetNextWordPair: increase topic index");

            vocabularyTopicIndex++;
            vocabularyWordIndex = -1;
        }

        vocabularyWordIndex++;

        // Obtain the next translation pair in the vocabulary dictionary based on index
        // rather than dictionary key
        string currentTopic = topics[vocabularyTopicIndex];
        Dictionary<string, string> topicTranslationDict = vocabularyDict[currentTopic];
        List<string> currentTopicTranslations = new List<string>(topicTranslationDict.Keys);
        string currentFrenchWord = currentTopicTranslations[vocabularyWordIndex];
        string currentEnglishWord = topicTranslationDict[currentFrenchWord];


        return new List<string> { currentFrenchWord, currentEnglishWord };
    }

    private List<string> GetPrevWordPair()
    {

        if ((vocabularyTopicIndex == 0) && (vocabularyWordIndex == 0))
        {
            Debug.Log("GetPrevWordPair: null");
            return null;
        }

        if ((vocabularyWordIndex <= 0) && (vocabularyTopicIndex > 0))
        {
            Debug.Log("GetPrevWordPair: decrease topic index");

            vocabularyTopicIndex--;
            string newTopic = topics[vocabularyTopicIndex];
            vocabularyWordIndex = (vocabularyDict[newTopic]).Count;
        }

        vocabularyWordIndex--;

        string currentTopic = topics[vocabularyTopicIndex];
        Dictionary<string, string> topicTranslationDict = vocabularyDict[currentTopic];
        List<string> currentTopicTranslations = new List<string>(topicTranslationDict.Keys);
        string currentFrenchWord = currentTopicTranslations[vocabularyWordIndex];
        string currentEnglishWord = topicTranslationDict[currentFrenchWord];


        return new List<string> { currentFrenchWord, currentEnglishWord };
    }


    public void NextCard()
    {
        List<string> currentPair = GetNextWordPair();

        if (currentPair != null)
        {
            UpdateText(currentPair[0], currentPair[1]);
        }
    }

    public void PrevCard()
    {
        List<string> currentPair = GetPrevWordPair();

        if (currentPair != null)
        {
            UpdateText(currentPair[0], currentPair[1]);
        }
    }
}
