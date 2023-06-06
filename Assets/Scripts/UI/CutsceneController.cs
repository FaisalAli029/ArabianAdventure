using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    // Set this to the name of your main menu scene
    public string mainMenuSceneName = "MainMenu";

    public void OnCutsceneFinished()
    {
        // Load the main menu scene
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
