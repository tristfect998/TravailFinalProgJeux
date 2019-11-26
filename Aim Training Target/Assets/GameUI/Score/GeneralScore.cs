using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GeneralScore : MonoBehaviour {
    Timer timeTimer;
    static int currentScore = 0;
    int scoreToGet;

    void Start()
    {
        scoreToGet = FindObjectsOfType<Target>().Length;
        currentScore = 0;
        timeTimer = FindObjectOfType<Timer>();
    }



    public int GetScore()
    {
        return currentScore;
    }

    public void AddScore()
    {
        currentScore += 1;


        if (currentScore >= scoreToGet)
        {
            
            showEndGame();
        }
    }

    private void showEndGame()
    {
        saveNewScore();
        SceneManager.LoadSceneAsync(4, LoadSceneMode.Single);
    }

    private void saveNewScore()
    {
      BinaryFormatter bf = new BinaryFormatter();
      FileStream file = File.Open(Application.persistentDataPath + "gameInfo.dat", FileMode.Create);
      gameData scoreDataToSave = new gameData();
      scoreDataToSave.mapIndex = SceneManager.GetActiveScene().buildIndex;
      scoreDataToSave.NewTimeToFinish = timeTimer.GetTimerTime();
      bf.Serialize(file, scoreDataToSave);
      file.Close();     
    }



}

[Serializable]
public class gameData
{
    public int mapIndex;
    public float NewTimeToFinish;
}