using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

public class EndGameButton : MonoBehaviour {
    int mapIndex;

    public void Restartmap()
    {
        SceneManager.LoadSceneAsync(EndGameController.EndGameControl.getmapindex(), LoadSceneMode.Single);
    }

    public void BackToMenue()
    {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }




}
