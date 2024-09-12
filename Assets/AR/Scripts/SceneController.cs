using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneController : MonoBehaviour
{
    public void GoToMainMenu()
    {
        StartCoroutine(LoadMainMenu());
        Debug.Log("SceneController.cs: Main Menu Screen is loading asynchroniously");
    }

    IEnumerator LoadMainMenu()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scene1"); // The Application loads the Scene in the background as the current Scene runs.

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void GoToScene2()
    {
        StartCoroutine(LoadScene2());
        Debug.Log("SceneController.cs: Scene2 is loading asynchroniously");
    }
    
    IEnumerator LoadScene2()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scene2"); //restarts the stage from the beginning, all progress is lost

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }


    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("SceneController.cs: Application has quit.");
    }
}
