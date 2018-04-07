using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    // Added these functions to our previous LevelManager script.
    public void LoadNextLevel() {
        // Load the next scene in the build order
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevel(string name){
        SceneManager.LoadScene(name);
    }

    public void EndGame () {
        Application.Quit();
    }
}
