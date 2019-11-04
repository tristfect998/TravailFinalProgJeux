using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {

    GeneralScore generalScore;
    Text scoreText;

    void Start () {
        generalScore = FindObjectOfType<GeneralScore>();
        scoreText = GetComponent<Text>();
    }

	void Update () {
        if (generalScore.GetScore() < 10)
        {
            scoreText.text = "SCORE : 0" + generalScore.GetScore().ToString();
        }
        else
        {
            scoreText.text = "SCORE : " + generalScore.GetScore().ToString();
        }
    }
}
