﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;

    Canvas pauseMenu;
    public static bool mainMenu = true;
    bool paused = false;
    // Use this for initialization

   

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        pauseMenu = GetComponentInChildren<Canvas>();
        DontDestroyOnLoad(this);
    }
    void Start ()
    {
        pauseMenu.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(!mainMenu)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                paused = !paused;

                if(paused)
                {
                    pauseMenu.gameObject.SetActive(true);
                    Time.timeScale = 0;
                    Cursor.lockState = CursorLockMode.Confined;
                }
                else
                {
                    pauseMenu.gameObject.SetActive(false);
                    Time.timeScale = 1;
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
	}

    public void returnToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
        mainMenu = true;
    }

    public void endGame()
    {
        Application.Quit();
    }

    public static void setMainMenu()
    {
        mainMenu = !mainMenu;
    }
}
