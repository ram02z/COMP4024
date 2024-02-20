using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    public TMP_Text wordText;
    // TODO: to be replaced by english-french translation pairs from text files, dynamically loaded depending on which themes are selected
    private string[] words = {"1","2","3","4","5","6","7","8","9","10"};
    private Queue<string> wordQueue = new();
    
    // Start is called before the first frame update
    void Start()
    {
        for (int word = 0; word < words.Length; word++)
        {
            wordQueue.Enqueue(words[word]);
        }
        UpdateWordUI();
        
    }

    void UpdateWordUI()
    {
        wordText.text = GetCurrentWord();
    }
    
    public string GetCurrentWord()
    {
        return wordQueue.Peek();
    }

    public void ChangeWord()
    {
        string word = wordQueue.Peek();
        wordQueue.Dequeue();
        wordQueue.Enqueue(word);
        UpdateWordUI();
    }

    public string GetRandomWord()
    {
        return words[Random.Range(0, words.Length)];
    }
}
