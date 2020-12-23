using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameSession : MonoBehaviour
{
    [Header("Scoring")]
    [SerializeField] int score = 0;
    [SerializeField] float timeScaler = 1;
    ScoreDisplay scoreDisplay;

    [Header("Player")]
    [SerializeField] GameObject playerPrefab;
    GameObject player;
    [SerializeField] int playerLives = 3;
    [SerializeField] float respawnTime = 2f;
    Vector2 spawnPosition;
    Quaternion spawnRotation;
    float respawnTimer = 2f;
    int currentLives = 3;
    bool respawning = false;


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
    public void ResetGameSession()
    {
        score = 0;
        currentLives = playerLives;
    }
    public void SetScoreDisplay()
    {
        scoreDisplay = FindObjectOfType<ScoreDisplay>();
    }
    private void Update()
    {
        Time.timeScale = timeScaler;
        OperateRespawnTimer();
    }

    // Player Operations

    public void RegisterPlayer(GameObject currentPlayer, Vector2 startPosition, Quaternion startRotation)
    {
        if (!player) 
        {
            player = currentPlayer;
            spawnPosition = startPosition;
            spawnRotation = startRotation;
            
        }
    }
    public void PlayerDied()
    {
        if (currentLives <= 0)
        {
            FindObjectOfType<SceneNav>().GetComponent<SceneNav>().LoadGameOver();
        }
        else
        {
            StartSpawnTimer();
            currentLives--;
        }
    }
    void StartSpawnTimer()
    {
        respawning = true;
    }
    void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, spawnPosition, spawnRotation);
    }
    void OperateRespawnTimer()
    {
        if (respawning)
        {
            respawnTimer -= Time.deltaTime;
            if (respawnTimer <= 0)
            {
                respawning = false;
                respawnTimer = respawnTime;
                SpawnPlayer();
            }
        }
    }
    public int GetPlayerLives()
    {
        return currentLives;
    }
}
