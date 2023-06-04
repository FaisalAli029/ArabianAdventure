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

    public PowerUpType powerUpType;
    public float duration = 10f; // duration in seconds

    private float remainingDuration = 0f;

    private bool isPowerUpActicated = false;

    private GameObject playerObject; // reference to the player object

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isPowerUpActicated)
        {
            playerObject = other.gameObject;

            // Pass power-up type to PlayerMovement script
            playerObject.GetComponent<PlayerMovement>().SetCurrentPowerUp(powerUpType, true);

            // Set remaining duration of the power-up
            remainingDuration = duration;

            Debug.Log("Power-up collected");

            isPowerUpActicated = true;

            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    private void Update()
    {
        // Debug.Log(remainingDuration);

        if (playerObject != null && remainingDuration > 0)
        {

            remainingDuration -= Time.deltaTime;

            // Check if the power-up effect should be disabled
            if (remainingDuration <= 0)
            {
                // Disable power-up effect
                playerObject.GetComponent<PlayerMovement>().SetCurrentPowerUp(PowerUpType.None, false);

                Debug.Log("Power Up disabled");
            }
        }
    }
}
