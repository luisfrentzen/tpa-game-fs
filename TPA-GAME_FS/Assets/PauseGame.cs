using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame

    public static bool isPaused = false;

    public GameObject PauseMenu;

    void Start()
    {
        Resume();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Quit()
    {
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    void Resume()
    {
        PauseMenu.SetActive(false);
        //Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        PauseMenu.SetActive(true);
        //Time.timeScale = 0f;
        isPaused = true;
    }
}
