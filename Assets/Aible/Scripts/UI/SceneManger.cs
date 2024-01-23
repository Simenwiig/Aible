using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManger : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;

    static bool isPaused;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {               
                pauseScreen.SetActive(true);
                PauseScene();
            }
            else
            {
                pauseScreen.SetActive(false);
                ResumeScene();
            }       
        }
    }

    public static void LoadScene(string name)
    {
        isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(name);
    }

    public static void RestartScene()
    {
        isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void PauseScene()
    {
        isPaused = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    public static void ResumeScene()
    {
        isPaused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
