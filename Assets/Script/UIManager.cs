using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public TMP_Text scoreText;
    public TMP_Text enemyCountText;
    public TMP_Text timerText;

    public void UpdateScore(int _score)
    {
        scoreText.text = "Score: " + _score.ToString();
    }
    public void UpdateEnemyCount(int _enemyCount)
    {
        enemyCountText.text = "Enemy Count: " + _enemyCount.ToString();
    }
    public void UpdateTimer(float _time)
    {
        timerText.text = _time.ToString("F2"); // F2 is the decimal point example F2 0.00, F1 0.0 
        if (_time <= 10)
        {
            timerText.color = Color.red;
        }
        else 
        {
            timerText.color = Color.white;
        }
    }
}
