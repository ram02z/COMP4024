using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using System.IO;
using System.Linq;

/*
 This is a helper class used to perform IO operations such as
reading vocabulary CSV files and listing filenames
*/
public class FileUtil : MonoBehaviour
{
    /*
     Reads the csv file with the given filepath and converts the contents
    to a disctionary of strings
     */
    public static Dictionary<string, string> ReadCSVFile(string fileName)
    {
        Dictionary<string, string> vocabulary = new Dictionary<string, string>();

        TextAsset csvData = Resources.Load<TextAsset>(fileName);

        if (csvData == null)
        {
            Debug.Log($"File not found: {fileName}");
            return vocabulary;
        }

        string[] lines = csvData.text.Split('\n');

        foreach (string line in lines.Skip(1))
        {
            string[] fields = line.Split(',');

            if (fields.Length > 2)
            {
                string english = string.Join(", ", fields, 1, fields.Length - 1).Trim();
                string french = fields[0].Trim();
                Debug.Log("french word: " + french);
                Debug.Log("english word: " + english);
                vocabulary.Add(french, english);
            }
            else if (fields.Length == 2)
            {
                string english = fields[1].Trim();
                string french = fields[0].Trim();
                vocabulary.Add(french, english);
            }
        }

        return vocabulary;
    }

    /*
     Returns a list of all the filenames in the CSV directory
    */
    public static List<string> GetFileNames(string directoryPath)
    {
        // Load all TextAssets from the given directory in the Resources folder
        TextAsset[] files = Resources.LoadAll<TextAsset>(directoryPath);

        // If no files were found, log a message and return null
        if (files.Length == 0)
        {
            Debug.Log("No files found in directory: " + directoryPath);
            return new List<string>();
        }

        // Extract the names of the files
        List<string> fileNames = new List<string>();
        foreach (TextAsset file in files)
        {
            fileNames.Add(file.name);
        }

        return fileNames;
    }

    /*
     * Writes the highscore to a file
     */
    public static void WriteHighScoreToFile(int highscore)
    {
        // Get the current date and time
        string dateTime = DateTime.Now.ToString();

        // Define the path where the highscore will be saved
        string path = Application.persistentDataPath + "/highscore.txt";
        
        Debug.Log($"Updated highscore to {highscore} in {path}");

        // Save the highscore and the current date and time to the file
        string dataToSave = highscore + ", " + dateTime + Environment.NewLine;
        File.AppendAllText(path, dataToSave);
    }
    
    /*
     * Reads the highscore from a file and
     * returns a list of tuples containing the highscore and the date and time it was achieved
     * in descending order
     */
    public static List<(int, string)> ReadHighScoresFromFile()
    {
        // Define the path where the highscore is saved
        string path = Application.persistentDataPath + "/highscore.txt";
        
        // Check if the file exists
        if (!File.Exists(path))
        {
            Debug.Log("Highscore file does not exist.");
            return new List<(int, string)>();
        }
        
        // Read all lines from the file
        string[] lines = File.ReadAllLines(path);

        // Create a list to store the highscores and datetimes
        List<(int, string)> highScores = new List<(int, string)>();

        // Each line in the array represents a highscore and the date and time it was achieved
        foreach (string line in lines)
        {
            // Split the line on the comma to separate the highscore and the date and time
            string[] parts = line.Split(',');

            // The first part is the highscore
            int highscore = int.Parse(parts[0]);

            // The second part is the date and time
            string dateTime = parts[1].Trim();

            // Add the highscore and datetime to the list
            highScores.Add((highscore, dateTime));
        }

        // Sort the list by highscore in descending order
        highScores.Sort((x, y) => y.Item1.CompareTo(x.Item1));

        return highScores;
    }
}