using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private GameOver gameOver;

    private void Awake()
    {
        gameOver = GetComponent<GameOver>();
    }
    public void ExitGame()
    {
        
        StartCoroutine(Co_WaitExitGame());
    }

    IEnumerator Co_WaitExitGame()
    {
        gameOver.FadeOut();
        yield return new WaitForSeconds(1.6f);
        Application.Quit();
    }
}
