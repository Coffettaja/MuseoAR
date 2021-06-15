#if UNITY_ANDROID
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using System.IO;
using System.Collections;
using UnityEngine.SceneManagement;

class VuforiaBuildProcessor : MonoBehaviour, IProcessScene, IPreprocessBuild, IPostprocessBuild {

    //private IEnumerator coroutine;

    //void Start() {
    //    coroutine = WaitAndLoad(3.0f);
    //    StartCoroutine(coroutine);
    //}

    //// Finds all obb-files and loads profileInput scene.
    //private IEnumerator WaitAndLoad(float waitTime) {
    //   // yield return StartCoroutine(ObbExtractor.ExtractObbDatasets());
    //    yield return new WaitForSeconds(waitTime);
    //    SceneManager.LoadScene("Splash");
    //}
    public int callbackOrder { get { return 0; } }
    static bool IsRunBuild = false;
    // OnPreprocessBuild -> OnProcessScene -> OnPostprocessBuild
    public void OnPreprocessBuild(BuildTarget target, string path) {
        IsRunBuild = true;
        if ( PlayerSettings.Android.useAPKExpansionFiles == true ) {
            string oldPath = Application.streamingAssetsPath + "/Vuforia";
            string newPath = Application.streamingAssetsPath + "/../VuforiaTemp";
            DirectoryInfo dirInfo = new DirectoryInfo(oldPath);
            if ( dirInfo.Exists && Directory.Exists(newPath) == false ) {
                Debug.Log("Preparation, transfer StreamingAssets/Vuforia to Assets/VuforiaTemp");
                dirInfo.MoveTo(newPath);
            }
        }
    }
    public void OnProcessScene(UnityEngine.SceneManagement.Scene scene) {
        if ( IsRunBuild ) {
            if ( PlayerSettings.Android.useAPKExpansionFiles == true ) {
                string oldPath = Application.streamingAssetsPath + "/../VuforiaTemp";
                string newPath = Application.streamingAssetsPath + "/../../Temp/StagingArea/assets/Vuforia";
                Debug.Log("Copy Assets/VuforiaTemp to " + newPath);
                DirectoryCopy(oldPath, newPath, true);
            }
        }
    }
    public void OnPostprocessBuild(BuildTarget target, string path) {
        if ( PlayerSettings.Android.useAPKExpansionFiles == true ) {
            string oldPath = Application.streamingAssetsPath + "/../VuforiaTemp";
            string newPath = Application.streamingAssetsPath + "/Vuforia";
            DirectoryInfo dirInfo = new DirectoryInfo(oldPath);
            if ( dirInfo.Exists && Directory.Exists(newPath) == false ) {
                Debug.Log("Returning folder Assets/VuforiaTemp back to StreamingAssets/Vuforia");
                dirInfo.MoveTo(newPath);
            }
        }
        IsRunBuild = false;
    }
    private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs) {
        // Get the subdirectories for the specified directory.
        DirectoryInfo dir = new DirectoryInfo(sourceDirName);
        if ( !dir.Exists ) {
            throw new DirectoryNotFoundException(
                "Source directory does not exist or could not be found: "
                + sourceDirName);
        }
        DirectoryInfo[] dirs = dir.GetDirectories();
        // If the destination directory doesn't exist, create it.
        if ( !Directory.Exists(destDirName) ) {
            Directory.CreateDirectory(destDirName);
        }
        // Get the files in the directory and copy them to the new location.
        FileInfo[] files = dir.GetFiles();
        foreach ( FileInfo file in files ) {
            if ( !file.Name.Contains(".meta") ) {
                string temppath = Path.Combine(destDirName, file.Name);
                if ( !File.Exists(temppath) )
                    file.CopyTo(temppath, false);
            }
        }
        // If copying subdirectories, copy them and their contents to new location.
        if ( copySubDirs ) {
            foreach ( DirectoryInfo subdir in dirs ) {
                string temppath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, temppath, copySubDirs);
            }
        }
    }
}
#endif //UNITY_ANDROID