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
    public CoinManager coinManager = CoinManager.Instance;

    private Button button;
    private int upgradeCount = 0;

    private void Start()
    {
        // Get a reference to the button component
        button = GetComponent<Button>();

        // Add a listener to the button click event
        button.onClick.AddListener(OnButtonClick);

        // Don't destroy this object when a new scene is loaded
        DontDestroyOnLoad(gameObject);
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
            coinManager.coins -= upgradeCost;
            powerUp.durations[powerUpType] += durationIncrease;
            upgradeCount++;

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

    private void Update()
    {
        // Check if this object is in a different scene than the power-up
        if (gameObject.scene.name != powerUp.gameObject.scene.name)
        {
            // If it is, find the corresponding power-up object in the current scene
            var powerUpObjects = FindObjectsOfType<PowerUp>();
            foreach (var powerUpObject in powerUpObjects)
            {
                if (powerUpObject.powerUpType == powerUpType)
                {
                    // Update the power-up reference
                    powerUp = powerUpObject;
                    break;
                }
            }
        }
    }
}
