using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneHandler : MonoBehaviour
{

    public static SceneHandler sceneHandler;
    private void Awake()
    {
        if (sceneHandler != null) Destroy(sceneHandler);
        else sceneHandler = this;
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
