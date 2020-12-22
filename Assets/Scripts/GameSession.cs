using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameSession : MonoBehaviour
{
    [SerializeField] int score = 0;
    [SerializeField] float timeScaler = 1;
    ScoreDisplay scoreDisplay;


    private void Awake()
    {
        SetupSingleton();
    }
    void SetupSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public int GetScore()
    {
        return score;
    }
    public void Score(int scoreValue)
    {
        if (!scoreDisplay) { SetScoreDisplay(); }
        score += scoreValue;
        scoreDisplay.UpdateScoreDisplay(score);
    }
    public void ResetScore()
    {
        score = 0;
        Debug.Log("ScoreReset");
    }
    public void SetScoreDisplay()
    {
        scoreDisplay = FindObjectOfType<ScoreDisplay>();
    }
    private void Update()
    {
        Time.timeScale = timeScaler;

    }
}
