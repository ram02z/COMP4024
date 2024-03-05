using UnityEngine;
using System.IO;

public class Hint : MonoBehaviour
{
    // Reference to the WordManager object
    public WordManager wordManager;

    // Reference to the AudioSource component (assign it in the Inspector)
    public AudioSource audioSource;

    // Function to play hint audio based on text
    public void PlayHint(string hintText)
    {
        // Use TextToSpeechHelper to get the filename associated with the hint text
        string filename = TextToSpeechHelper.GetMapping(hintText);

        // Check if a filename is found
        if (!string.IsNullOrEmpty(filename))
        {
            string file = filename.Substring(0, filename.Length - 4);
            PlayAudio(file);
        }
        else
        {
            Debug.LogWarning("No hint found for the given text: " + hintText);
        }
    }

    // Function to play audio using the AudioSource
    void PlayAudio(string path)
    {
        // Load the AudioClip using Resources Load
        AudioClip clip = Resources.Load<AudioClip>(path);
      
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogError($"Failed to load audio clip {path}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the H key is pressed
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (wordManager == null)
            {
                wordManager = FindObjectOfType<WordManager>();
            }

            if (wordManager != null)
            {
                string hintText = wordManager.GetCurrentFrenchWord();
                PlayHint(hintText);
            }
            else
            {
                Debug.LogWarning("WordManager object not found in the scene!");
            }
        }
    }
}
