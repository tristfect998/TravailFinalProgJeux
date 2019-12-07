using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

public class EndGameController : MonoBehaviour
{
    public static EndGameController EndGameControl;

    List<gameDataHighScore> DataHighScore = new List<gameDataHighScore>();
    float bestTimeToFinish;
    int mapIndex;
    float newTimeToFinish;
    Timer timer;
    bool hasAHighScore;
    AudioSource audioSrc;
    public AudioClip WinSound;

    void Start()
    {
        if (EndGameControl == null)
        {
            EndGameControl = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioSrc = GetComponent<AudioSource>();
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

        if (!File.Exists(Application.persistentDataPath + "gameInfoHighRecs.dat"))
        {
            throw new Exception("Game file doesnt exist");
        }

        FileStream file = File.Open(Application.persistentDataPath + "gameInfoHighRecs.dat", FileMode.Open);
        scoreData scoreDataToLoad = (scoreData)bf.Deserialize(file);
        bestTimeToFinish = scoreDataToLoad.gameHighscore.Where(s => s.mapIndex == mapIndex).FirstOrDefault().bestTimeToFinish;
        DataHighScore = scoreDataToLoad.gameHighscore;
        file.Close();
    }
  
    public void SaveFirstScore()
    {
        FileStream file = File.Open(Application.persistentDataPath + "gameInfoHighRecs.dat", FileMode.Create);

        try
        {
            BinaryFormatter bf = new BinaryFormatter();       
            scoreData scoreDataToSave = new scoreData();
            scoreDataToSave.gameHighscore =  DataHighScore;
            bf.Serialize(file, scoreDataToSave);
            file.Close();
        }
        catch (Exception e)
        {
            file.Close();
            throw new Exception(e.ToString());
        }
       
    }

    public void SaveHasHighScore()
    {
        FileStream file = File.Open(Application.persistentDataPath + "gameInfoHighRecs.dat", FileMode.Create);

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            scoreData scoreDataToSave = new scoreData();
            DataHighScore.Where(s => s.mapIndex == mapIndex).FirstOrDefault().bestTimeToFinish = newTimeToFinish;
            scoreDataToSave.gameHighscore = DataHighScore;
            bf.Serialize(file, scoreDataToSave);
            file.Close();
        }
        catch (Exception e)
        {
            file.Close();
            throw new Exception(e.ToString());
        }

    }


    public void isScoreBeat()
    {
        if (newTimeToFinish < bestTimeToFinish)
        {
            audioSrc.PlayOneShot(WinSound);
            SaveHasHighScore();
        }
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 32;
        GUI.Label(new Rect(165, 90, 180, 80), "Arène terminée : " + mapIndex, style);
        GUI.Label(new Rect(165, 140, 180, 80), "Votre temps: " + String.Format("{0:0:00}", newTimeToFinish), style);
        GUI.Label(new Rect(165, 190, 180, 80), "Le temps record: " + String.Format("{0:0:00}", bestTimeToFinish), style);

        if (newTimeToFinish > bestTimeToFinish)
        {
            GUI.Label(new Rect(165, 240, 180, 80), "Vous n'avez pas battu votre record" , style);
        }
        else
        {
            GUI.Label(new Rect(165, 240, 180, 80), "Vous avez battu votre record", style);
        }
    }

    public void addHighScore()
    {
        gameDataHighScore Data = new gameDataHighScore();
        Data.mapIndex = mapIndex;
        Data.bestTimeToFinish = newTimeToFinish;
        DataHighScore.Add(Data);
    }

    public void Restartmap()
    {
        SceneManager.LoadSceneAsync(mapIndex, LoadSceneMode.Single);
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }

    public int getmapindex()
    {
        return mapIndex;
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
    public int mapIndex;
    public float bestTimeToFinish;
}