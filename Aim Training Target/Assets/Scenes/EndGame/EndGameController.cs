using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class EndGameController : MonoBehaviour {

    public List<gameDataHighScore> DataHighScore;
    float bestTimeToFinish;
    int mapIndex;
    float newTimeToFinish;
    Timer timer;
    bool hasAHighScore;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        LoadDernierScore();
        try
        {
            LoadMeilleurScore();
            hasAHighScore = true;
        }
        catch (Exception)
        {
            hasAHighScore = false;
            bestTimeToFinish =0.00f;
        }

        if (hasAHighScore)
        {
            isScoreBeat();
        }
        else
        {
            addHighScore();
            SaveFirstScore();
        }

    }


    public void LoadDernierScore()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (!File.Exists(Application.persistentDataPath + "gameInfo.dat"))
        {
            throw new Exception("Game file doesnt exist");
        }
        FileStream file = File.Open(Application.persistentDataPath + "gameInfo.dat", FileMode.Open);
        gameData scoreDataToLoad = (gameData)bf.Deserialize(file);
        newTimeToFinish  = scoreDataToLoad.NewTimeToFinish;
        mapIndex = scoreDataToLoad.mapIndex;
        file.Close();
    }

    public void LoadMeilleurScore()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (!File.Exists(Application.persistentDataPath + "gameInfo.dat"))
        {
            throw new Exception("Game file doesnt exist");
        }
        FileStream file = File.Open(Application.persistentDataPath + "gameInfoHighScores.dat", FileMode.Open);
        scoreData scoreDataToLoad = (scoreData)bf.Deserialize(file);
        bestTimeToFinish = scoreDataToLoad.gameHighscore[mapIndex].bestTimeToFinish;
        file.Close();
    }
  
    public void SaveFirstScore()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "gameInfoHighRecord.dat", FileMode.Create);
        scoreData scoreDataToSave = new scoreData();
        scoreDataToSave.gameHighscore = DataHighScore;
        bf.Serialize(file, scoreDataToSave);
        file.Close();
    }

    public void SaveHasHighScore()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "gameInfoHighRecord.dat", FileMode.Create);
        scoreData scoreDataToSave = new scoreData();
        scoreDataToSave.gameHighscore[mapIndex].bestTimeToFinish = newTimeToFinish;
        bf.Serialize(file, scoreDataToSave);
        file.Close();
    }


    public void isScoreBeat()
    {
        if (newTimeToFinish > bestTimeToFinish)
        {
            SaveHasHighScore();
        }
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 32;
        GUI.Label(new Rect(165, 90, 180, 80), "Arene terminer : " + mapIndex, style);
        GUI.Label(new Rect(165, 140, 180, 80), "Votre temps: " + newTimeToFinish, style);
        GUI.Label(new Rect(165, 190, 180, 80), "Le temps records: " + bestTimeToFinish, style);
        if (newTimeToFinish < bestTimeToFinish)
        {
            GUI.Label(new Rect(165, 240, 180, 80), "Vous n'avez pas battu votre records" , style);
        }
        else
        {
            GUI.Label(new Rect(165, 240, 180, 80), "Vous avez battu votre records", style);
        }
    }

    public void addHighScore()
    {
        gameDataHighScore Time = new gameDataHighScore();
        Time.bestTimeToFinish = newTimeToFinish;
        DataHighScore.Add(Time);
    }


}


[Serializable]
public class scoreData
{
    public List<gameDataHighScore> gameHighscore;
}


[Serializable]
public class gameDataHighScore
{
    public float bestTimeToFinish;
}


