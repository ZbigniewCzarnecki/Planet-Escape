using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string DIRECTORY_PATH = Application.dataPath + "/Saves/";
    private static readonly string FILE_NAME = "SaveFile_01.json";

    public static void Save(string fileContent)
    {
        //Create Folder If Not Exists
        if (!Directory.Exists(DIRECTORY_PATH))
        {
            Directory.CreateDirectory(DIRECTORY_PATH);
        }

        //Save File
        File.WriteAllText(DIRECTORY_PATH + FILE_NAME, fileContent);
    }

    public static string Load()
    {
        //Load File
        if (File.Exists(DIRECTORY_PATH + FILE_NAME))
        {
            string saveString = File.ReadAllText(DIRECTORY_PATH + FILE_NAME);
            return saveString;
        }
        else
        {
            return null;
        }
    }
}
