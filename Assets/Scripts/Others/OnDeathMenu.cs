using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class OnDeathMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject DeathMenuUI;


    
    void Update()
    {
    }
    // Update is called once per frame

    public void Restart()
    {
        DeathMenuUI.SetActive(false);
        SceneManager.LoadScene("level1");
        GameIsPaused = false;
    }
    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
