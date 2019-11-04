using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralScore : MonoBehaviour {

    int currentScore = 0;

    public int GetScore()
    {
        return currentScore;
    }

    public void AddScore()
    {
        currentScore += 1;
    }
}