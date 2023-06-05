using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class PowerupButton : MonoBehaviour
{
    public PowerUp.PowerUpType powerUpType;
    public PowerUp powerUp;
    public float durationIncrease = 5f;
    public int upgradeCost = 10;

    private Button button;
    private int upgradeCount = 0;

    private void Start()
    {
        // Get a reference to the button component
        button = GetComponent<Button>();

        // Add a listener to the button click event
        button.onClick.AddListener(OnButtonClick);

        // Load the upgrade count for this power-up from PlayerPrefs
        if (PlayerPrefs.HasKey(powerUpType.ToString() + "UpgradeCount"))
        {
            upgradeCount = PlayerPrefs.GetInt(powerUpType.ToString() + "UpgradeCount");
        }
    }

    private void OnDestroy()
    {
        // Remove the listener from the button click event
        button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        // Check if the player has enough coins to purchase an upgrade and hasn't reached the upgrade limit
        if (CoinManager.Instance.coins >= upgradeCost && upgradeCount < 5)
        {
            // Deduct coins, increase the duration of the power-up and increment the upgrade count
            CoinManager.Instance.DeductCoins(upgradeCost);
            powerUp.UpgradeDuration(durationIncrease);
            upgradeCount++;

            // Save the upgrade count for this power-up to PlayerPrefs
            PlayerPrefs.SetInt(powerUpType.ToString() + "UpgradeCount", upgradeCount);
            PlayerPrefs.Save();

            Debug.Log("Duration for " + powerUpType.ToString() + " power-up changed to " + powerUp.durations[powerUpType].ToString() + " seconds. Upgrade count: " + upgradeCount.ToString());
        }
        else if (upgradeCount >= 5)
        {
            Debug.Log("Upgrade limit reached for " + powerUpType.ToString() + " power-up!");
        }
        else
        {
            Debug.Log("Not enough coins to purchase an upgrade!");
        }
    }
}
