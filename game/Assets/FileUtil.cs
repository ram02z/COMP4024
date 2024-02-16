using System.Collections.Generic;
using UnityEngine;
using System.IO;

/*
 This will be a helper class to perform IO operations such as
reading vocabulary csv files and listing filenames
*/
public class FileUtil:MonoBehaviour
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

                    if (fields.Length >= 2)
                    {
                        string french = fields[0].Trim();
                        string english = fields[1].Trim();

                        vocabulary.Add(french, english);
                    }
                }
            }
        }
        catch (IOException e)
        {
            Debug.LogError($"Error reading CSV file: {e.Message}");
        }

        return vocabulary;
    }
}
