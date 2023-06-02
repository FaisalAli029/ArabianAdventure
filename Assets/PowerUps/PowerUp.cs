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
        Flying,
        None
    }

    public PowerUpType powerUpType;
    public float duration = 3.0f; // duration in seconds
    public bool hasPowerUp = false;

    private IEnumerator DisablePowerUpAfterDuration()
    {
        Debug.Log("Power-Up disbaled");

        yield return new WaitForSeconds(duration);

        // Disable power-up effect after duration has elapsed
        hasPowerUp = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hasPowerUp = true;

            other.gameObject.GetComponent<PlayerMovement>().powerUp = this;

            // Pass power-up type to PlayerMovement script
            other.gameObject.GetComponent<PlayerMovement>().SetCurrentPowerUp(powerUpType);

            // Set duration of the power-up
            StartCoroutine(DisablePowerUpAfterDuration());

            Debug.Log("Power-up collected");

            this.gameObject.SetActive(false);
        }
    }
}
