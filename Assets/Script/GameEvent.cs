using System;
using UnityEngine;

public class GameEvent
{
    public static event Action<GameObject> OnEnemyHit = null;
    public static event Action<GameObject> OnEnemyDied = null;
    public static event Action<GameObject> OnPlayerHit = null;
    public static event Action<GameObject> OnPlayerDied = null;
    public static event Action<Difficulty> OnDifficultyChange = null;

    public static void ReportEnemyHit(GameObject _enemy)
    {
        Debug.Log("Enemy " + _enemy.name + " was hit");
        OnEnemyHit?.Invoke(_enemy);
    }

    public static void ReportEnemyDied(GameObject _enemy)
    {
        Debug.Log("Enemy " + _enemy.name + " has died");
        OnEnemyDied?.Invoke(_enemy);
    }

    public static void ReportPlayerHit(GameObject _player)
    {
        OnPlayerHit?.Invoke(_player);
        ReportGameStateChange(GameState.GameOver);
    }

    public static void ReportPlayerDied(GameObject _player)
    {
        OnPlayerDied?.Invoke(_player);
    }

    public static void ReportDifficultyChange(Difficulty _difficulty)
    {
        OnDifficultyChange?.Invoke(_difficulty);
    }

    public static void ReportGameStateChange(GameState _gameState)
    {

    }
}
