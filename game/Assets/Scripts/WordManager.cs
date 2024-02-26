using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WordChangedEvent: UnityEvent<string> {}

// This class is responsible for managing the words that are displayed in the game.
public class WordManager : MonoBehaviour
{
    private Vocabulary vocabulary; // Reference to the Vocabulary component.
    private List<string> words = new(); // The list of words.
    private Queue<string> wordQueue = new(); // The queue of words.
    public WordChangedEvent onWordChanged = new(); // Event that is invoked when the word changes.

    // This method is called at the start of the game.
    // It enqueues all the words into the wordQueue and updates the wordText.
    void Start()
    {
        vocabulary = FindObjectOfType<Vocabulary>();
        if (vocabulary != null)
        {
            words = new List<string>(vocabulary.GetVocabMap().Keys);
            foreach (var word in words)
            {
                wordQueue.Enqueue(word);
            }
            onWordChanged.Invoke(GetCurrentEnglishWord());
        }
        else
        {
            Debug.LogError("Vocabulary object not found");
        }
    }

    // This method returns the current word from the wordQueue.
    public string GetCurrentFrenchWord()
    {
        return wordQueue.Peek();
    }

    // This method returns the current word from the wordQueue in English.
    private string GetCurrentEnglishWord()
    {
        return vocabulary.GetEnglishTranslation(GetCurrentFrenchWord());
    }

    // This method changes the current word in the wordQueue and updates the wordText.
    public void ChangeWord()
    {
        string word = wordQueue.Peek();
        wordQueue.Dequeue();
        wordQueue.Enqueue(word);
        onWordChanged.Invoke(GetCurrentEnglishWord());
    }

    // This method returns a random word from the vocabulary.
    public string GetRandomWord()
    {
        return words[Random.Range(0, words.Count)];
    }
}