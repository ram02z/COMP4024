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
    public static Dictionary<string, string> ReadCSVFile(string filePath)
    {
        Dictionary<string, string> vocabulary = new Dictionary<string, string>();
        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] fields = line.Split(',');

                    if (fields.Length > 2)
                    {
                        string english = string.Join(", ", fields, 1, fields.Length - 1).Trim();
                        string french = fields[0].Trim();
                        Debug.Log("french word is " + french);
                        Debug.Log("english word is " + english);
                        vocabulary.Add(french, english);
                    }
                    else if (fields.Length == 2)
                    {
                        string english = fields[1].Trim();
                        string french = fields[0].Trim();
                        vocabulary.Add(french, english);
                    }

                }
            }
        }
        catch (IOException e)
        {
            Debug.Log($"Error reading CSV file: {e.Message}");
        }

        return vocabulary;
    }

    /*
     Returns a list of all the filenames in the CSV directory
    */
    public static List<string> GetFileNames(string directoryPath)
    {
        if (Directory.Exists(directoryPath))
        {
            List<string> filePaths = Directory.GetFiles(directoryPath).ToList();
            List<string> fileNames = new List<string>();
            fileNames = filePaths.Where(filePath => !filePath.EndsWith(".meta")).Select(Path.GetFileName).ToList();

            return fileNames;
        }
        else
        {
            Debug.Log("Directory does not exist: " + directoryPath);
            return null;
        }
    }

    /*
     * Writes the highscore to a file
     */
    public static void WriteHighScoreToFile(int highscore)
    {
        // Get the current date and time
        string dateTime = DateTime.Now.ToString(CultureInfo.CurrentCulture);

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