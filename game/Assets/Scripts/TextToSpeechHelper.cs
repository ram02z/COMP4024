using System;
using System.IO;
using System.Text;
using UnityEngine;

public class TextToSpeechHelper : MonoBehaviour
{
    public static string GetMapping(string originalText)
    {
        // Define the file path (adjust as needed)
        const string mappingFilePath = "Assets/Resources/text_to_mp3_mapping.txt";

        // Attempt to open the file and read lines
        if (!File.Exists(mappingFilePath))
        {
            return null; // File not found
        }

        using (StreamReader reader = new StreamReader(mappingFilePath, Encoding.UTF8))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
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
        }

        return null; // No match found or other issues
    }
}
