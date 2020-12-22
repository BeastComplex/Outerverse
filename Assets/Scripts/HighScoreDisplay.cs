using UnityEngine;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
    TextMeshProUGUI scoreDisplay;

    void Start()
    {
        scoreDisplay = GetComponent<TextMeshProUGUI>();
        UpdateScoreDisplay();
    }
    void UpdateScoreDisplay()
    {
        scoreDisplay.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
}
