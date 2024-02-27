using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * This class contains the logic responsible for cycling through
 * translation pairs in the vocabMap contained by the vocabulary object.
 * It maintains a vocabularyTopicIndex and vocabularyWordIndex
 * representing the currently selected transation pair. These values
 * are used to update the englishText and frenchText to reflect
 * the currently selected translation pair text.
 */
public class RevisionTextController : MonoBehaviour
{
    public TextMeshProUGUI englishText;
    public TextMeshProUGUI frenchText;
    private int vocabularyTopicIndex = 0;
    private int vocabularyWordIndex = -1;
    private Dictionary<string, Dictionary<string, string>> vocabularyDict;
    private List<string> topics;
    Dictionary<string, string> topicTranslationDict;
    List<string> currentTopicTranslations;

    /*
     * Update the englishText and frenchText components with the
     * first entry in the vocabularyDict
     */
    public void Start()
    {
        Vocabulary vocabulary = FindObjectOfType<Vocabulary>();
        vocabularyDict = vocabulary.GetVocabMap();
        topics = new List<string>(vocabularyDict.Keys);
        UpdateCurrentTopic();
        NextCard();
    }

    /*
     * Changes the text displayed by the englishText and frenchText components
     */
    private void UpdateText(string english, string french)
    {
        englishText.text = english;
        frenchText.text = french;
    }

    /*
     * Update the topicTranslationDict and currentTopicTranslations variables
     * to reflect the currently selected topic
     */
    private void UpdateCurrentTopic()
    {
        string currentTopic = topics[vocabularyTopicIndex];
        topicTranslationDict = vocabularyDict[currentTopic];
        currentTopicTranslations = new List<string>(topicTranslationDict.Keys);
    }

    /*
     * Calculates the next vocabularyWordIndex and vocabularyTopicIndex then
     * selects the appropriate french and english words from the vocabularyDict.
     * If at the end of the vocabularyDict then returns null.
     */
    private List<string> GetNextWordPair()
    {
        // Check if the current index is the last entry in the entire vocab dictionary
        int maxIndexOfFinalTopic = vocabularyDict[topics[topics.Count - 1]].Count - 1;
        if ((vocabularyTopicIndex == topics.Count - 1) && 
            (vocabularyWordIndex == maxIndexOfFinalTopic))
        {
            return null;
        }

        // Check if the current index is the last entry in the current vocab topic
        int maxIndexOfCurrentTopic = vocabularyDict[topics[vocabularyTopicIndex]].Count - 1;
        if ((vocabularyWordIndex == maxIndexOfCurrentTopic) &&
            (vocabularyTopicIndex < topics.Count - 1))
        {
            vocabularyTopicIndex++;
            vocabularyWordIndex = -1;
            UpdateCurrentTopic();
        }

        vocabularyWordIndex++;
        string currentFrenchWord = currentTopicTranslations[vocabularyWordIndex];
        string currentEnglishWord = topicTranslationDict[currentFrenchWord];

        return new List<string> { currentEnglishWord, currentFrenchWord };
    }

    /*
     * Calculates the previous vocabularyWordIndex and vocabularyTopicIndex then
     * selects the appropriate french and english words from the vocabularyDict.
     * If at the start of the vocabularyDict then returns null.
     */
    private List<string> GetPrevWordPair()
    {

        // Check if the current index is the first entry in the entire vocab dictionary
        if ((vocabularyTopicIndex == 0) && (vocabularyWordIndex == 0))
        {
            return null;
        }

        // Check if the current index is the first entry in the current vocab topic
        if ((vocabularyWordIndex <= 0) && (vocabularyTopicIndex > 0))
        {
            vocabularyTopicIndex--;
            string newTopic = topics[vocabularyTopicIndex];
            vocabularyWordIndex = (vocabularyDict[newTopic]).Count;
            UpdateCurrentTopic();
        }

        vocabularyWordIndex--;
        string currentFrenchWord = currentTopicTranslations[vocabularyWordIndex];
        string currentEnglishWord = topicTranslationDict[currentFrenchWord];

        return new List<string> { currentEnglishWord, currentFrenchWord };
    }

    /*
     * Updates the text displayed by the englishText and frenchText components
     * to be that of the next entry in the vocabularyDict if one exists
     */
    public void NextCard()
    {
        List<string> currentPair = GetNextWordPair();

        if (currentPair != null)
        {
            UpdateText(currentPair[0], currentPair[1]);
        }
    }

    /*
     * Updates the text displayed by the englishText and frenchText components
     * to be that of the previous entry in the vocabularyDict if one exists
     */
    public void PrevCard()
    {
        List<string> currentPair = GetPrevWordPair();

        if (currentPair != null)
        {
            UpdateText(currentPair[0], currentPair[1]);
        }
    }
}
