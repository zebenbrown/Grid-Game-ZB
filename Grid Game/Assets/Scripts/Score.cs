using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    private int score = 0;
    private int highScore;

    private void Awake()
    {
        DisplayScore();
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.Save();

        if (score > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + " High score", 0))
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + " High score", score);
            PlayerPrefs.Save();
            highScoreText.text = "High Score: " + score.ToString();
        }

        UpdateScoreDisplay();
    }

    public void DisplayScore()
    {
        Debug.Log((SceneManager.GetActiveScene().name));
        score = PlayerPrefs.GetInt("score", 0);
        string activeSceneName = SceneManager.GetActiveScene().name;

        if (activeSceneName == "Regular Difficulty")
        {
            scoreText.text = "Score: " + score;
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt("Regular Difficulty High score", 0);
        }

        else if (activeSceneName == "Hard Difficulty")
        {
            scoreText.text = "Score: " + score;
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt("Hard Difficulty High score", 0);

        }
        else if (activeSceneName == "Regular Difficulty Game Over")
        {
            scoreText.text = "Score: " + score;
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt("Regular Difficulty High score", 0);
        }
        else if (activeSceneName == "Hard Difficulty Game Over")
        {
            scoreText.text = "Score: " + score;
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt("Hard Difficulty High score", 0);
        }
    }

    public void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + score.ToString();
        PlayerPrefs.GetInt("score", score);

    }
}
