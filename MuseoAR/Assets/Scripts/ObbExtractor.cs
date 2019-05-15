﻿using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObbExtractor
{

    public static IEnumerator ExtractObbDatasets()
    {
        // string[] files = Vuforia.TrackerManager.Instance.GetStateManager()
        // TODO database names
        string[] filesInOBB = { ".dat", ".xml" };
        string[] vuforiaDB = { "360Videos", "Aarteenmetsastys", "quizDB", "Reset", "SpaceInvaders", "TLDR" };
        foreach (var v in vuforiaDB)
        {
            foreach (var filename in filesInOBB)
            {
                string united = v + filename;

                string uri = Application.streamingAssetsPath + "/Vuforia/" + united;

                string outputFilePath = Application.persistentDataPath + "/Vuforia/" + united;
                if (!Directory.Exists(Path.GetDirectoryName(outputFilePath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(outputFilePath));

                var www = new WWW(uri);
                yield return www;

                Save(www, outputFilePath);
                yield return new WaitForEndOfFrame();
            }
        }
        

    }

    private static void Save(WWW w, string outputPath)
    {
        File.WriteAllBytes(outputPath, w.bytes);

        // Verify that the File has been actually stored
        if (File.Exists(outputPath))
        {
            Debug.Log("File successfully saved at: " + outputPath);
        }
        else
        {
            Debug.Log("Failure!! - File does not exist at: " + outputPath);
        }
    }
}