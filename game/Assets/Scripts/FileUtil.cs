using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

/*
 This is a helper class used to perform IO operations such as
reading vocabulary CSV files and listing filenames
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

    /*
     Returns a list of all the filenames in the CSV directory
    */
    public static List<string> GetFileNames() {
        string directoryPath = "Assets/CSV";

            if (Directory.Exists(directoryPath))
            {
                List<string> filePaths = Directory.GetFiles(directoryPath).ToList();
                List<string> fileNames = new List<string>();
                fileNames = filePaths.Where(filePath => !filePath.EndsWith(".meta")).Select(Path.GetFileName).ToList();

            return fileNames;
            }
            else{
                Debug.LogError("Directory does not exist: " + directoryPath);
                return null;
            }
    }
}
