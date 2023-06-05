using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType
    {
        DestroyObstacle,
        DoubleJump,
        Magnet,
        Speed,
        None
    }

    public Dictionary<PowerUpType, float> durations = new Dictionary<PowerUpType, float>();

    private float remainingDuration;

    private bool isPowerUpActivated = false;

    private GameObject playerObject; // reference to the player object

    private void Start()
    {
        // Set default durations for all power-ups if they haven't been set yet
        foreach (PowerUpType powerUpType in Enum.GetValues(typeof(PowerUpType)))
        {
            if (PlayerPrefs.HasKey(powerUpType.ToString()))
            {
                if (PlayerPrefs.GetFloat(powerUpType.ToString()) < 10.0f)
                {
                    PlayerPrefs.SetFloat(powerUpType.ToString(), 10.0f);
                    PlayerPrefs.Save();
                }
            }
            durations[powerUpType] = PlayerPrefs.GetFloat(powerUpType.ToString());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isPowerUpActivated)
        {
            playerObject = other.gameObject;

            // Pass power-up type to PlayerMovement script
            playerObject.GetComponent<PlayerMovement>().SetCurrentPowerUp(GetPowerUpType(), true);

            // Set remaining duration of the power-up
            remainingDuration = durations[GetPowerUpType()];

            Debug.Log(remainingDuration);

            Debug.Log("Power-up collected");

            isPowerUpActivated = true;

            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void Update()
    {
        if (playerObject != null && remainingDuration > 0)
        {
            remainingDuration -= Time.deltaTime;

            // Check if the power-up effect should be disabled
            if (remainingDuration <= 0)
            {
                // Disable power-up effect
                playerObject.GetComponent<PlayerMovement>().SetCurrentPowerUp(PowerUpType.None, false);

                isPowerUpActivated = false;

                Debug.Log("Power Up disabled");
            } 
        }
    }

    // Helper method to get the current power-up type
    private PowerUpType GetPowerUpType()
    {
        if (this.gameObject.CompareTag("PowerUp1"))
        {
            return PowerUpType.DestroyObstacle;
        }
        else if (this.gameObject.CompareTag("PowerUp3"))
        {
            return PowerUpType.DoubleJump;
        }
        else if (this.gameObject.CompareTag("PowerUp4"))
        {
            return PowerUpType.Magnet;
        }
        else if (this.gameObject.CompareTag("PowerUp2"))
        {
            return PowerUpType.Speed;
        }
        else
        {
            return PowerUpType.None;
        }
    }

    // Method to upgrade the duration of the power-up
    public void UpgradeDuration(float addition)
    {
        PowerUpType powerUpType = GetPowerUpType();
        float originalDuration = durations[powerUpType];
        float upgradedDuration = originalDuration + addition;

        // Save the upgraded duration to PlayerPrefs
        PlayerPrefs.SetFloat(powerUpType.ToString(), upgradedDuration);

        durations[powerUpType] = upgradedDuration;

        Debug.Log($"Upgraded {powerUpType} duration to {upgradedDuration}");

        PlayerPrefs.Save();
    }
}
