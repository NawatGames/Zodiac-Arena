using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    [SerializeField] private GameObject pauseMenu;
    private int sceneIndex;
    private float lastTimeScale = 1;

    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!paused);
            if(!paused)
                lastTimeScale = Time.timeScale;
            Time.timeScale = paused ? lastTimeScale : 0;
            paused = !paused;
        }
        else if(paused && Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMainMenu");
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = lastTimeScale;
        paused = false;
    }
}
