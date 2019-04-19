using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas pauseMenu;
    public static bool mainMenu = true;
    bool paused = false;
	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(pauseMenu);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!mainMenu)
        {
            if(Input.GetKey(KeyCode.Escape))
            {
                paused = !paused;

                if(paused)
                {
                    pauseMenu.gameObject.SetActive(true);
                    Time.timeScale = 0;
                }
                else
                {
                    pauseMenu.gameObject.SetActive(false);
                    Time.timeScale = 1;
                }
            }
        }
	}

    public void returnToMenu()
    {
        SceneManager.LoadScene(0);
        pauseMenu.gameObject.SetActive(false);
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
