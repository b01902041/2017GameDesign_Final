using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public GameObject fix, _canvas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetString ("Mode") == "level") {
			PlayerPrefs.SetString ("Mode", " ");
			PlayerPrefs.Save ();
			LevelBtn ();
		}
	}

	public void PlayButton(){
		PlayerPrefs.SetString ("Mode", "level");
	}

	public void QuitButton(){
		Application.Quit();
	}

	void LevelBtn(){
		for (int i = 1; i < 4; i++){
			GameObject Target = Resources.Load ("Level" + i) as GameObject;
			fix = Instantiate (Target, Vector3.zero, Quaternion.identity) as GameObject;
		}
	}

}
