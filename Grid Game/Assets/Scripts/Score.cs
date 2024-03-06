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
        
            if (score > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name, 0))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, score);
                PlayerPrefs.Save();
                highScoreText.text = "High Score: " + score.ToString();
            }

            UpdateScoreDisplay();
    }

    public void DisplayScore()
    {
        score = PlayerPrefs.GetInt("score", 0);
        scoreText.text = "Score: " + PlayerPrefs.GetInt("score", 0).ToString(); 
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt(SceneManager.GetActiveScene().name, 0).ToString();
        
    }
    
    public void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + score.ToString();
        PlayerPrefs.GetInt("score", score);
        
    }
}