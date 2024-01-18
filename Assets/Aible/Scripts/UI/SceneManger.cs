using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManger : MonoBehaviour
{
    public static void LoadScene(string name)
    {

        name = SceneManager.GetActiveScene().name;
        

        SceneManager.LoadSceneAsync(name);
    }

    public static void RestartScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
