using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text scoreText;
    public Text coinText;
    private int score;

    void Start()
    {
        score = 00000000;
        InvokeRepeating("UpdateScore", Time.deltaTime, Time.deltaTime);
        InvokeRepeating("UpdateCoin", Time.deltaTime, Time.deltaTime);
    }

    void UpdateScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    void UpdateCoin()
    {
        coinText.text = "Coins: " + PlayerManager.numberOfCoins;
    }
}