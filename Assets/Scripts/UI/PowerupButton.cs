using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using TMPro;

public class PowerupButton : MonoBehaviour
{
    public PowerUp.PowerUpType powerUpType;
    public PowerUp powerUp;
    public float durationIncrease = 5f;
    public int upgradeCost = 10;
    private Button button;
    private int upgradeCount = 0;

    public TextMeshProUGUI upgradeCountText;
    public TextMeshProUGUI messageText;

    private CoinManager coinManager;

    private void Start()
    {
        // Get a reference to the button component
        button = GetComponent<Button>();

        coinManager = FindAnyObjectByType<CoinManager>();

        // Add a listener to the button click event
        button.onClick.AddListener(OnButtonClick);

        // Load the upgrade count for this power-up from PlayerPrefs
        if (PlayerPrefs.HasKey(powerUpType.ToString() + "UpgradeCount"))
        {
            upgradeCount = PlayerPrefs.GetInt(powerUpType.ToString() + "UpgradeCount");
        }

        // Update the updateCountText
        upgradeCountText.text = "Upgrades Left: " + (5 - upgradeCount);
    }

    private void OnDestroy()
    {
        // Remove the listener from the button click event
        button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        // Check if the player has enough coins to purchase an upgrade and hasn't reached the upgrade limit
        if (coinManager.coins >= upgradeCost && upgradeCount < 5)
        {
            // Deduct coins, increase the duration of the power-up and increment the upgrade count
            coinManager.DeductCoins(upgradeCost);
            coinManager.UpdateCoinText();
            powerUp.UpgradeDuration(durationIncrease);
            upgradeCount++;

            // Save the upgrade count for this power-up to PlayerPrefs
            PlayerPrefs.SetInt(powerUpType.ToString() + "UpgradeCount", upgradeCount);
            PlayerPrefs.Save();

            // Update the updateCountText
            upgradeCountText.text = "Upgrades Left: " + (5 - upgradeCount);

            if (messageText != null)
            {
                messageText.text = "Duration for " + powerUpType.ToString() + " power-up changed to " + powerUp.durations[powerUpType].ToString() + " seconds. Upgrade count: " + upgradeCount.ToString();
            }
        }
        else if (upgradeCount >= 5)
        {
            if (messageText != null)
            {
                messageText.text = "Upgrade limit reached for " + powerUpType.ToString() + " power-up!";
            }
        }
        else
        {
            if (messageText != null)
            {
                messageText.text = "Not enough coins to purchase an upgrade!";
            }
        }
    }
}
