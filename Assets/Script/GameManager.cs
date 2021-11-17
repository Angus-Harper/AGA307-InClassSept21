using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Title,
    Playing,
    Paused,
    GameOver
}

public enum Difficulty
{
    Easy,
    Normal,
    Hard
}
public class GameManager : Singleton<GameManager>
{
    public GameState gameState;
    public Difficulty difficulty;

    public int score;
    int scoreMultiplier = 1;

    public float maxTime = 30f;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        ChangeDifficulty();
        timer = maxTime;
    }
    void Update()
    {
        if (gameState == GameState.Playing)
        {
            timer -= Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, maxTime);
            _UI.UpdateTimer(timer);
        }
    }
    public void ChangeDifficulty()
    {
        switch(difficulty)
        {
            case Difficulty.Easy:
                scoreMultiplier = 1;
                maxTime = 100f;
                break;
            case Difficulty.Normal:
                scoreMultiplier = 2;
                maxTime = 75;
                break;
            case Difficulty.Hard:
                scoreMultiplier = 3;
                maxTime = 30f;
                break;
        }
    }
    
    public void ChangeGameState(GameState _gameState)
    {
        gameState = _gameState;
    }
    public void AddScore(int _score)
    {
        score += _score * scoreMultiplier;
        _UI.UpdateScore(score);
    }

    private void OnEnable()
    {
        GameEvent.OnEnemyHit += OnEnemyHit;
        GameEvent.OnEnemyDied += OnEnemyDied;
    }

    private void OnDisable()
    {
        GameEvent.OnEnemyHit -= OnEnemyHit;
        GameEvent.OnEnemyDied -= OnEnemyDied;
    }

    void OnEnemyHit(GameObject _enemy)
    {
        AddScore(10);
    }

    void OnEnemyDied(GameObject _enemy)
    {
        AddScore(100);
    }
}
