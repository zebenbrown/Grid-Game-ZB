using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SimonSays : MonoBehaviour
{
    public SwitchScene switchScene;
    public Score scoreScript;

    [SerializeField] private GridManager gridManager;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI PatternPlayingText;

    private List<Vector2Int> correctPosition = new List<Vector2Int>();

    private bool PatternPlaying;
    private int playerPatternIndex;
    public int score;
    public int highScore;


    private void Awake()
    {
        PlayerPrefs.DeleteKey("score");
        score = 0;
        scoreText.text = "Score: " + score.ToString();
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("High Score", highScore).ToString();

    }
    
    private void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        scoreScript.UpdateScoreDisplay();
    }

    private void OnEnable()
    {
        gridManager.TileSelected += OnTileSelected;
    }

    private void OnDisable()
    {
        gridManager.TileSelected -= OnTileSelected;
    }

    private void OnTileSelected(GridTile gridTile)
    {
        if (PatternPlaying)
        {
            return;
        }
        
        if (gridTile.gridCoordinates == correctPosition[playerPatternIndex])
        {
            Debug.Log("Correct");
            //scoreText.text = "Score: " + score.ToString();
            StartCoroutine(Co_FlashTile(gridTile, Color.green, 0.25f));
            playerPatternIndex++;
            if (playerPatternIndex == correctPosition.Count)
            {
                NextPattern();
                scoreScript.addScore(1);
                scoreScript.UpdateScoreDisplay();
                
                
            }
        }
        else
        {
            Scene activeScene = SceneManager.GetActiveScene();
            string currentScene = activeScene.name;
            if (currentScene == "Regular Difficulty")
            {
                score = 0;
                Debug.Log("Wrong");
                StartCoroutine(Co_FlashTile(gridTile, Color.red, 0.25f));
                correctPosition.Clear();
                //NextPattern();
                switchScene.LoadScene("Regular Difficulty Game Over");
                
            }
            
            else if (currentScene == "Hard Difficulty")
            {
                score = 0;
                Debug.Log("Wrong");
                StartCoroutine(Co_FlashTile(gridTile, Color.red, 0.25f));
                correctPosition.Clear();
                //NextPattern();
                switchScene.LoadScene("Hard Difficulty Game Over");
            }
        }
    }
    
    [ContextMenu("Next Pattern")]
    public void NextPattern()
    {
        score = 0;
        playerPatternIndex = 0;
        correctPosition.Add(new Vector2Int(Random.Range(0, gridManager.numColumns), Random.Range(0, gridManager.numRows)));
        StartCoroutine(Co_PlayPattern(correctPosition));
    }

    private IEnumerator Co_PlayPattern(List<Vector2Int> positions)
    {
        PatternPlaying = true;
        PatternPlayingText.text = "Wait, Pattern Playing";
        yield return new WaitForSeconds(1f);
        foreach (var pos in positions)
        {
            GridTile tile = gridManager.GetTile(pos);
            yield return Co_FlashTile(tile, Color.red, 0.25f);
            yield return new WaitForSeconds(0.5f);
        }

        PatternPlaying = false;
        PatternPlayingText.text = "Click The Tiles in The Correct Pattern";
    }

    private IEnumerator Co_FlashTile(GridTile tile, Color color, float duration)
    {
        tile.SetColor(color);
        yield return new WaitForSeconds(duration);
        tile.ResetColor();
    }
}
