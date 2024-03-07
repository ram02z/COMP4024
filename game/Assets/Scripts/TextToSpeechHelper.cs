using System;
using System.IO;
using System.Text;
using UnityEngine;

public class TextToSpeechHelper : MonoBehaviour
{
    public static string GetMapping(string originalText)
    {
        TextAsset mappingFile = Resources.Load<TextAsset>("text_to_mp3_mapping");

        // Check if the file was loaded successfully
        if (mappingFile == null)
        {
            return null; // File not found
        }

        // Split the text into lines
        string[] lines = mappingFile.text.Split('\n');

        foreach (string line in lines)
        {
            // Split the line based on the delimiter
            string[] parts = line.Trim().Split(new string[] { " | " }, StringSplitOptions.None);

            // Extract text and filename
            string lineText = parts[0];
            string filename = parts.Length > 1 ? parts[1] : null;

            // Check for matching text
            if (originalText == lineText)
            {
                return filename;
            }
        }

        return null; // No match found or other issues
    }
}
