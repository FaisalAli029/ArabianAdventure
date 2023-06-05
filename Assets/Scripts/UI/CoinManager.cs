using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int coins = 0;

    // Static instance of the CoinManager
    public static CoinManager Instance { get; private set; }

    private void Awake()
    {
        // Ensure that there is only one instance of the CoinManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }

    public void DeductCoins(int amount)
    {
        coins -= amount;
    }
}
