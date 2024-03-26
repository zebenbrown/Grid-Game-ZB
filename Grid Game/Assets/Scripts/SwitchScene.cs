using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    private GameOver gameOver;
    private void Awake()
    {
        gameOver = GetComponent<GameOver>();
    }
    public void LoadScene(string sceneName)
    {
        if (SceneManager.GetActiveScene().name == "Regular Difficulty Game Over")
        {
            gameOver.FadeOut();
            StartCoroutine(Co_WaitLoadScene(sceneName)); 
            return;
        }
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator Co_WaitLoadScene(string sceneName)
    {
        yield return new WaitForSeconds(1.6f);
        SceneManager.LoadScene(sceneName);
    }
}
