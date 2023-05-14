using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    public Slider slider;
    public float maxPower = 100f;

    private float currentPower;

    private void Start()
    {
        currentPower = maxPower;
        UpdatePowerBar();
    }

    public void UpgradePowerUp(float amount)
    {
        currentPower += amount;
        currentPower = Mathf.Clamp(currentPower, 0f, maxPower);
        UpdatePowerBar();
    }

    private void UpdatePowerBar()
    {
        slider.value = currentPower / maxPower;
    }
}
