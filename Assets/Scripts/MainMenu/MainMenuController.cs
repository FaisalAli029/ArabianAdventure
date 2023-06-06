using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene("ShopUI");
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
    public void GoBack()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Play()
    {
        SceneManager.LoadScene("Levels");
    }

    public void Controls()
    {
        SceneManager.LoadScene("ControlsUI");
    }
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
        ScoreController.Instance.SetScoreEnabled(true);
    }
        public void LoadLevel2()
    {
        SceneManager.LoadScene("Level 1");
        ScoreController.Instance.SetScoreEnabled(true);
    }
        public void LoadLevel3()
    {
        SceneManager.LoadScene("Level 1");
        ScoreController.Instance.SetScoreEnabled(true);
    }
        public void LoadLevel4()
    {
        SceneManager.LoadScene("Level 1");
        ScoreController.Instance.SetScoreEnabled(true);
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
