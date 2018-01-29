using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seer : MonoBehaviour {
    private int min;
    private int max;
    private int guess;
    private LevelManager levelManager;

    public int attempts;
    public Text guessLabel;

	// Use this for initialization
	void Start () {
        levelManager = FindObjectOfType<LevelManager>();
        StartGame();
        ShowGuess();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartGame(){
        min = 1;
        max = 1001;
        NextGuess();
    }

    void NextGuess(){
        guess = Random.Range(min, max);
        attempts--;
    }

    void ShowGuess(){
        if (attempts >= 0)
        {
            guessLabel.text = "Is your number " + guess + "?";
        }
        else
        {
            levelManager.LoadLevel("Win");
        }
    }

    public void GuessHiger(){
        min = guess + 1;
        NextGuess();
        ShowGuess();
    }

    public void GuessLower(){
        max = guess;
        NextGuess();
        ShowGuess();
    }

    public void CorrectGuess(){
        levelManager.LoadLevel("Lose");
    }


}
