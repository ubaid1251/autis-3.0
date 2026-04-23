using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class JsonFileWriter : MonoBehaviour
{
    public static void SaveToTextFile(string fileName, string jsonContent)
    {
        string filePath = Application.persistentDataPath + "/" + fileName + ".txt";
        File.WriteAllText(filePath, jsonContent);
        Debug.Log("Saved JSON to: " + filePath);
    }

    public static string LoadFromTextFile(string fileName)
    {
        string filePath = Application.persistentDataPath + "/" + fileName + ".txt";

        if (File.Exists(filePath))
            return File.ReadAllText(filePath);

        Debug.LogWarning("File not found: " + filePath);
        return null;
    }

    // ========== Resources folder (Editor-only write, runtime read) ==========
#if UNITY_EDITOR
    public static void SaveToResources(string relativePath, string jsonContent)
    {
        string fullFolderPath = Path.Combine(Application.dataPath, "Resources");

        // Handle subfolders (e.g. "SortRack/1")
        string fullFilePath = Path.Combine(fullFolderPath, relativePath + ".txt");

        // Ensure the folder exists
        string directory = Path.GetDirectoryName(fullFilePath);
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        // Write the file
        File.WriteAllText(fullFilePath, jsonContent);

        // Refresh so Unity can see the new asset
        AssetDatabase.Refresh();
        Debug.Log("Saved JSON to: " + fullFilePath);
    }
#endif

    public static string LoadFromResources(string fileName)
    {
        TextAsset file = Resources.Load<TextAsset>(fileName);

        if (file != null)
            return file.text;

        Debug.LogWarning("File not found in Resources: " + fileName);
        return null;
    }

}
