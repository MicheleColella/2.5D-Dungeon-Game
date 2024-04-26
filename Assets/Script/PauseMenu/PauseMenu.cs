using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;
    public GameObject OptionMenu;

    public GameObject pausemenuUI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pausemenuUI.SetActive(false);
        GameIsPaused = false;
    }

    public void Pause()
    {
        pausemenuUI.SetActive(true);
        GameIsPaused = true;
    }

    public void Options()
    {
        Debug.Log("Impostazioni");
        OptionMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Esci");
        Application.Quit();
    }
}
