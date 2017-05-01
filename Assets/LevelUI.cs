using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Reset(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	public void Level(){
		PlayerPrefs.SetString ("Mode", "level");
		SceneManager.LoadScene (0);
	}
	public void Menu(){
		PlayerPrefs.SetString ("Mode", " ");
		SceneManager.LoadScene (0);
	}
}
