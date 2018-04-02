using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;		// Required to switch scenes
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public void LoadLevel(string name) {

        //Debug.Log ("New Level load: " + name);
        //	Application.LoadLevel (name);    -- This method was deprecated a long time ago
        SceneManager.LoadScene(name);
    }

    public void EndGame() {
        //Debug.Log ("Quit requested");
        Application.Quit();
    }

    public void GameS() {
        LoadLevel("Game");
    }

    public void Menu() {
        LoadLevel("StartG");
    }

    // Added these functions to our previous LevelManager script.
    public void LoadNextLevel() {
        // Load the next scene in the build order
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
