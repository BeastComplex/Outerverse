using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    TextMeshProUGUI scoreDisplay;
    GameSession gameSession;

    
    // Start is called before the first frame update
    void Start()
    {
        scoreDisplay = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
        gameSession.SetScoreDisplay();
        UpdateScoreDisplay(gameSession.GetScore());
    }
    public void UpdateScoreDisplay(int score)
    {
        scoreDisplay.text = score.ToString();
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
}
