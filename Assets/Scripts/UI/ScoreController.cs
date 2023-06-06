using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{
    public TextMeshPro scoreText;
    public TextMeshPro coinText;

    private int score;
    private string levelIndex;

    private int highestScoreLevel1;
    private int highestScoreLevel2;
    private int highestScoreLevel3;
    private int highestScoreLevel4;

    public bool isScoreEnabled = true; // Flag to enable/disable the score

    void Start()
    {
        levelIndex = SceneManager.GetActiveScene().name;

        highestScoreLevel1 = PlayerPrefs.GetInt("HighestScoreLevel1", 0);
        highestScoreLevel2 = PlayerPrefs.GetInt("HighestScoreLevel2", 0);
        highestScoreLevel3 = PlayerPrefs.GetInt("HighestScoreLevel3", 0);
        highestScoreLevel4 = PlayerPrefs.GetInt("HighestScoreLevel4", 0);

        score = 0;
        InvokeRepeating("UpdateScore", 0f, 1f);
        InvokeRepeating("UpdateCoin", 0f, 1f);
    }

    void UpdateScore()
    {
        if (scoreText != null)
        {
            if (isScoreEnabled)
            {
                score++;
                scoreText.text = "Score: " + score;

                if (score > GetHighestScore())
                {
                    SetHighestScore(score);
                }
            }
        }
    }

    void UpdateCoin()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + PlayerManager.numberOfCoins;
        }
    }

    public int GetHighestScore()
    {
        switch (levelIndex)
        {
            case "Level 1":
                return highestScoreLevel1;
            case "Level 2":
                return highestScoreLevel2;
            case "Level 3":
                return highestScoreLevel3;
            case "Level 4":
                return highestScoreLevel4;
            default:
                return 0;
        }
    }

    public int GetHighestScoreWithString(string level)
    {
        switch (level)
        {
            case "Level 1":
                return highestScoreLevel1;
            case "Level 2":
                return highestScoreLevel2;
            case "Level 3":
                return highestScoreLevel3;
            case "Level 4":
                return highestScoreLevel4;
            default:
                return 0;
        }
    }

    public void SetHighestScore(int score)
    {
        switch (levelIndex)
        {
            case "Level 1":
                highestScoreLevel1 = score;
                PlayerPrefs.SetInt("HighestScoreLevel1", score);
                PlayerPrefs.Save();
                break;
            case "Level 2":
                highestScoreLevel2 = score;
                PlayerPrefs.SetInt("HighestScoreLevel2", score);
                PlayerPrefs.Save();
                break;
            case "Level 3":
                highestScoreLevel3 = score;
                PlayerPrefs.SetInt("HighestScoreLevel3", score);
                PlayerPrefs.Save();
                break;
            case "Level 4":
                highestScoreLevel4 = score;
                PlayerPrefs.SetInt("HighestScoreLevel4", score);
                PlayerPrefs.Save();
                break;
            default:
                break;
        }
    }

    // New method to enable/disable the score
    public void SetScoreEnabled(bool enabled)
    {
        isScoreEnabled = enabled;
    }
}
