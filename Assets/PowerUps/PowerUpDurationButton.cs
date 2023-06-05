using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDurationButton : MonoBehaviour
{
    public PowerUp.PowerUpType powerUpType;
    public PowerUp powerUp;

    private void Start()
    {
        // Don't destroy this object when a new scene is loaded
        DontDestroyOnLoad(gameObject);
    }

    public void OnButtonClick()
    {
        // Increase the duration of the power-up by 5 seconds
        powerUp.durations[powerUpType] += 5f;

        Debug.Log("Duration for " + powerUpType.ToString() + " power-up changed to " + powerUp.durations[powerUpType].ToString() + " seconds.");
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
                    // Update the power-up reference and duration
                    powerUp = powerUpObject;
                    break;
                }
            }
        }
    }
}
