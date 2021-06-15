using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AndroidSplitLoadFirstScene : MonoBehaviour {

	//Only use in Preloader Scene for android split APK
	private string nextScene = "Splash";

	public Texture2D background;
	public GUISkin mySkin;
	private bool obbisok = false;
	private bool loading = false;
	private bool replacefiles = false; //true if you wish to over copy each time
	string[] filesInOBB = { ".dat", ".xml" };
	string[] vuforiaDB = { "360Videos", "Aarteenmetsastys", "quizDB", "Reset", "SpaceInvaders", "TLDR" };
	private string[] paths ={
								"QCAR/yourtargets.dat",
								"QCAR/yourtargets.xml",
								"QCAR/Unity.txt",
								"mymovie1.m4v",
								"mymovie2.m4v"
							};

	void Update() {
		if ( Application.platform == RuntimePlatform.Android ) {
			if ( Application.dataPath.Contains(".obb") && !obbisok ) {
				StartCoroutine(CheckSetUp());
				obbisok = true;
			}
		}
		else {
			if ( !loading ) {
				StartApp();
			}
		}
	}

	void OnGUI() {
		GUI.skin = mySkin;
		GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.BeginVertical();
		GUILayout.FlexibleSpace();
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Label(background, GUILayout.Width(background.width), GUILayout.Height(background.height));
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		if ( !obbisok ) {
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.Label("There is an installation error with this application, Please re-install!");
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}

	public void StartApp() {
		loading = true;
		SceneManager.LoadScene(nextScene);
	}

	public IEnumerator CheckSetUp() {
		//Check and install!
		for ( int i = 0; i < paths.Length; ++i ) {
			yield return StartCoroutine(PullStreamingAssetFromObb(paths[i]));
		}
		yield return new WaitForSeconds(1f);
		StartApp();
	}

	//Alternatively with movie files these could be extracted on demand and destroyed or written over
	//saving device storage space, but creating a small wait time.
	public IEnumerator PullStreamingAssetFromObb(string sapath) {
		if ( !File.Exists(Application.persistentDataPath + sapath) || replacefiles ) {
			WWW unpackerWWW = new WWW(Application.streamingAssetsPath + "/" + sapath);
			while ( !unpackerWWW.isDone ) {
				yield return null;
			}
			if ( !string.IsNullOrEmpty(unpackerWWW.error) ) {
				Debug.Log("Error unpacking:" + unpackerWWW.error + " path: " + unpackerWWW.url);

				yield break; //skip it
			}
			else {
				Debug.Log("Extracting " + sapath + " to Persistant Data");

				if ( !Directory.Exists(Path.GetDirectoryName(Application.persistentDataPath + "/" + sapath)) ) {
					Directory.CreateDirectory(Path.GetDirectoryName(Application.persistentDataPath + "/" + sapath));
				}
				File.WriteAllBytes(Application.persistentDataPath + "/" + sapath, unpackerWWW.bytes);
				//could add to some kind of uninstall list?
			}
		}
		yield return 0;
	}
}
