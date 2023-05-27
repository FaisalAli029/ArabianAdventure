using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{

    public static int numberOfCoins;
    public static int totalCoins;

    public TextMeshProUGUI coinsText;
    // Start is called before the first frame update
    void Start()
    {
        numberOfCoins = 0;
    }

    // Update is called once per frame
    void Update() { 
        coinsText.text = "Coins: " + numberOfCoins;
    }

    public void AddAdditionalCoins(int coins)
    {
        totalCoins += coins;
    }





}
