using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    public GameObject PVEMenu;
    public GameObject PVPMenu;
    public GameObject MainMenu;
    public GameObject QuitMenu;
    public GameObject JoinMenu;
    private string oldSceneName;

    public void ChangeLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0) 
        {
            oldSceneName = SceneManager.GetActiveScene().name;
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex).completed += ChangeLevel_completed;
            QuitMenu.SetActive(true);
        }
    }
    private void ChangeLevel_completed(AsyncOperation obj)
    {
        QuitMenu.SetActive(false);
        if (oldSceneName.Contains("PVE"))
        {
            PVEMenu.SetActive(true);
        }
        else if (oldSceneName.Contains("PVP"))
        {
            PVPMenu.SetActive(true);
        }
        else
        {
            MainMenu.SetActive(true);
        }
    }

    public void JoinLevel(int SceneIndex)
    {
        SceneManager.LoadSceneAsync(SceneIndex, LoadSceneMode.Single).completed += JoinLevel_completed;
        JoinMenu.SetActive(true);
    }
    private void JoinLevel_completed(AsyncOperation obj)
    {
        JoinMenu.SetActive(false);
    }
}
