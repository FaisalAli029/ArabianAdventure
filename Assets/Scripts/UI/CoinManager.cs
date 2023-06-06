using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private static CoinManager instance;

    public TextMeshProUGUI coinText;

    public static CoinManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CoinManager>();
                if (instance == null)
                {
                    Debug.LogError("No CoinManager instance found in the scene.");
                }
            }
            return instance;
        }
    }

    public int coins = 0;

    private void Start()
    {
        // Load the player's coin balance from PlayerPrefs
        if (PlayerPrefs.HasKey("Coins"))
        {
            coins = PlayerPrefs.GetInt("Coins");
        }

        // Update the coin text
        UpdateCoinText();
    }

    public void AddCoins(int amount)
    {
        coins += amount;

        // Update the coin text and save the new coin balance to PlayerPrefs
        //UpdateCoinText();
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.Save();
    }

    public void DeductCoins(int amount)
    {
        coins -= amount;

        // Update the coin text and save the new coin balance to PlayerPrefs
        UpdateCoinText();
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.Save();
    }

    private void UpdateCoinText()
    {
        coinText.text = "Total Coins: " + coins.ToString();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
