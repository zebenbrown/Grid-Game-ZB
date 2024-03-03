using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    public void addScore(int scoreToAdd)
    {
        score += scoreToAdd;
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.Save();
        if (score > PlayerPrefs.GetInt("High Score", 0))
        {
                    PlayerPrefs.SetInt("High Score", score);
                    PlayerPrefs.Save();
                    highScoreText.text = "High Score: " + score.ToString();
        }
        UpdateScoreDisplay();
    }

    public void DisplayScore()
    {
        //score = GetComponent<SimonSays>().score;
        //highScore = GetComponent<SimonSays>().highScore;
        score = PlayerPrefs.GetInt("score", 0);
        scoreText.text = "Score: " + PlayerPrefs.GetInt("score", 0).ToString();
        highScoreText.text = "High Score:" + PlayerPrefs.GetInt("High Score", 0).ToString();
    }
    
    public void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + score.ToString();
        PlayerPrefs.GetInt("score", score);
        
    }
}