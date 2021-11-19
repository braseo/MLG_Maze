using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;


    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        //restart normal time = play
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        //Stop the time = pause
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync("MainScene");
        SceneManager.LoadScene("Menu");
       
    }

    public void Quit()
    {
        Application.Quit();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
                //GameObject.Find("InGameUI").SetActive(true);
            }
            else
            {
                //GameObject.Find("InGameUI").SetActive(false);
                Pause();
            }
        }
    }
}
