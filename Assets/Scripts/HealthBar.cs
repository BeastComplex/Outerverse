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

    // PlayerLives Display
    [SerializeField] Image[] livesDisplay;
    [SerializeField] Sprite life;
    [SerializeField] Sprite noLife;
    int lives;
    public void UpdatePlayerLives(int newLives)
    {
        if (newLives > livesDisplay.Length) { newLives = livesDisplay.Length; }
        for (int i = 0; i < livesDisplay.Length; i++)
        {
            if (i >= newLives)
            {
                livesDisplay[i].sprite = noLife;
            }
            else
            {
                livesDisplay[i].sprite = life;
            }
        }
    }
}
