using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharScore : MonoBehaviour {

    PainChar pc = new PainChar();

    void Start() {
        Text scoreText = GetComponent<Text>();
        scoreText.text = pc.score.ToString();
        // scoreText.text = ScoreKeeper.score.ToString ();
        pc.Reset();
    }

}
