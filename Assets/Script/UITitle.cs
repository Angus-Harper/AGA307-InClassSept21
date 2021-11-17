using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITitle : GameBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
        _GM.ChangeGameState(GameState.Playing);
    }

    // Update is called once per frame
    public void QuitGame()
    {
        Application.Quit();
    }
}
