using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider slider;

    public void SetupHealthBar(int maxHealth)
    {
        if (!slider) { slider = GetComponent<Slider>(); }
        slider.maxValue = maxHealth;
    }
    public void UpdateHealth(int currentHealth)
    {
        slider.value = currentHealth;
    }
}
