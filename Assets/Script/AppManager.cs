using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour
{
    public GameObject losepanel;
    public void NextScene()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Quitapp()
    {
        Application.Quit();
    }

    public void gamepaused()
    {
        Time.timeScale = 0;
        losepanel.SetActive(true);
    }

    public void resumeGame()
    {
        losepanel.SetActive(false);
        Time.timeScale = 1;
    }
}
