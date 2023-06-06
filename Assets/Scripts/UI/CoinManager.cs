using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    public int coins;

    private void Start()
    {
        // Load the player's coin balance from PlayerPrefs
        if (PlayerPrefs.HasKey("Coins"))
        {
            coins = PlayerPrefs.GetInt("Coins");
        }

        if (coinText != null)
        {
            // Update the coin text
            UpdateCoinText();
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;

        // Update the coin text and save the new coin balance to PlayerPrefs
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.Save();
    }

    public void DeductCoins(int amount)
    {
        coins -= amount;

        // Update the coin text and save the new coin balance to PlayerPrefs
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.Save();
    }

    public void UpdateCoinText()
    {
        coinText.text = "Total Coins: " + PlayerPrefs.GetInt("Coins");
    }
}
