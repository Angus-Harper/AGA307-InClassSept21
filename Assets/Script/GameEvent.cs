using System;
using UnityEngine;

public class GameEvent
{
    public static event Action<GameObject> OnEnemyHit = null;
    public static event Action<GameObject> OnEnemyDied = null;
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

    public static void ReportDifficultyChange(Difficulty _difficulty)
    {
        OnDifficultyChange?.Invoke(_difficulty);
    }
}
