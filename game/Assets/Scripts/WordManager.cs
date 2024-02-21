using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WordChangedEvent: UnityEvent<string> {}

// This class is responsible for managing the words that are displayed in the game.
public class WordManager : MonoBehaviour
{
    // TODO: to be replaced by english-french translation pairs from text files, dynamically loaded depending on which themes are selected
    private string[] words = {"1","2","3","4","5","6","7","8","9","10"}; // The array of words.
    private Queue<string> wordQueue = new(); // The queue of words.
    public WordChangedEvent onWordChanged = new(); // Event that is invoked when the word changes.

    // This method is called at the start of the game.
    // It enqueues all the words into the wordQueue and updates the wordText.
    void Start()
    {
        for (int word = 0; word < words.Length; word++)
        {
            wordQueue.Enqueue(words[word]);
        }
        onWordChanged.Invoke(GetCurrentWord());
    }

    // This method returns the current word from the wordQueue.
    public string GetCurrentWord()
    {
        return wordQueue.Peek();
    }

    // This method changes the current word in the wordQueue and updates the wordText.
    public void ChangeWord()
    {
        string word = wordQueue.Peek();
        wordQueue.Dequeue();
        wordQueue.Enqueue(word);
        onWordChanged.Invoke(GetCurrentWord());
    }

    // This method returns a random word from the words array.
    public string GetRandomWord()
    {
        return words[Random.Range(0, words.Length)];
    }
}