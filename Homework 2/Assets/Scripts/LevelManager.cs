using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Needed to load scenes

public class LevelManager : MonoBehaviour {

    //  This method will allow us to jump to a specific scene, using its name.
    public void LoadLevel(string levelName) {
        SceneManager.LoadScene(levelName);
    }

    //  This method will terminate the game application in any platform but Web.
    public void EndGame() {
        Application.Quit();
    }

}
