using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float duration = 10f; // The duration of the power-up effect
    public string obstacleTag = "Obstacle"; // The tag assigned to obstacle objects

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("PowerUp1"))
            {
                StartCoroutine(ApplyPowerUp1());
            }
            else if (gameObject.CompareTag("PowerUp2"))
            {
                StartCoroutine(ApplyPowerUp2());
            }
            else if (gameObject.CompareTag("PowerUp3"))
            {
                StartCoroutine(ApplyPowerUp3());
            }
            else if (gameObject.CompareTag("PowerUp4"))
            {
                StartCoroutine(ApplyPowerUp4());
            }
            // Destroy the power-up object
            Destroy(gameObject);
        }
    }

    public IEnumerator ApplyPowerUp1()
    {
        // Apply power-up 1 effect
        Debug.Log("Power-up 1 activated");

        // Disable obstacle objects
        Collider[] obstacleColliders = Physics.OverlapSphere(transform.position, 10f);
        foreach (Collider collider in obstacleColliders)
        {
            if (collider.CompareTag(obstacleTag))
            {
                collider.gameObject.SetActive(false);
            }
        }

        yield return new WaitForSeconds(duration);

        // Enable obstacle objects
        foreach (Collider collider in obstacleColliders)
        {
            if (collider.CompareTag(obstacleTag))
            {
                collider.gameObject.SetActive(true);
            }
        }
    }

    public IEnumerator ApplyPowerUp2()
    {
        // Apply power-up 2 effect
        Debug.Log("Power-up 2 activated");

        // TODO: Implement power-up 2 effect

        yield return new WaitForSeconds(duration);

        // TODO: Implement power-up 2 effect

    }

    public IEnumerator ApplyPowerUp3()
    {
        // Apply power-up 3 effect
        Debug.Log("Power-up 3 activated");

        // TODO: Implement power-up 3 effect

        yield return new WaitForSeconds(duration);

        // TODO: Implement power-up 3 effect

    }

    public IEnumerator ApplyPowerUp4()
    {
        // Apply power-up 4 effect
        Debug.Log("Power-up 4 activated");

        // TODO: Implement power-up 4 effect

        yield return new WaitForSeconds(duration);

        // TODO: Implement power-up 4 effect

    }
}
