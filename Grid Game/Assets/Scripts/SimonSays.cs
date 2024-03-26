using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.WSA;
using Random = UnityEngine.Random;

public class SimonSays : MonoBehaviour
{
    public FadeScript fadeScript;
    public SwitchScene switchScene;
    public Score scoreScript;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI PatternPlayingText;

    private List<Vector2Int> correctPosition = new List<Vector2Int>();

    private bool PatternPlaying;
    private static int playerPatternIndex = 0;
    private static int redFlashCheck = 0;
    public int score;
    public int highScore;
    


    private void Awake()
    {
        PlayerPrefs.DeleteKey("score");
        score = 0;
        scoreText.text = "Score: " + score.ToString();
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("High Score", highScore).ToString();

    }
    private void Start()
    {
        
        fadeScript = GetComponent<FadeScript>();
        if (fadeScript == null)
        {
            Debug.Log("FadeScript not found");
        }
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
        
        if (playerPatternIndex < correctPosition.Count && gridTile.gridCoordinates == correctPosition[playerPatternIndex])
        {
            
            StartCoroutine(Co_FlashTile(gridTile, Color.green, 0.25f));
            playerPatternIndex++;   
            if (playerPatternIndex == correctPosition.Count)
            {
                NextPattern();
                scoreScript.AddScore(1);
                scoreScript.UpdateScoreDisplay();
            }
        }

        else
        {
            if (SceneManager.GetActiveScene().name == "Regular Difficulty")
            {
                score = 0;
                Debug.Log("Wrong");
                StartCoroutine(Co_FlashTile(gridTile, Color.red, 0.25f));
                correctPosition.Clear();
                fadeScript.CanvasFade();
                gridManager.GridFade();
                StartCoroutine(Co_WaitRegularDifficulty());
            }
            
            else if (SceneManager.GetActiveScene().name == "Hard Difficulty")
            {
                score = 0;
                Debug.Log("Wrong");
                StartCoroutine(Co_FlashTile(gridTile, Color.red, 0.25f));
                correctPosition.Clear();
                fadeScript.CanvasFade();
                StartCoroutine(Co_WaitHardDifficulty());
               
            }

            playerPatternIndex = 0;
        }
    }
    
    /*private void OnTileSelected(GridTile gridTile)
    {
        if (PatternPlaying)
        {
            return;
        }
        
        if (playerPatternIndex < correctPosition.Count && gridTile.gridCoordinates == correctPosition[playerPatternIndex])
        {
            int randomRed = 3;
            Debug.Log("Correct");
            if (playerPatternIndex > 0)
            {
                randomRed = Random.Range(0, playerPatternIndex + 1);
                Debug.Log(randomRed);
            }
            StartCoroutine(Co_FlashSequence(gridTile, randomRed, 0.25f));
            playerPatternIndex++;
            if (playerPatternIndex == correctPosition.Count)
            {
                NextPattern();
                scoreScript.AddScore(1);
                scoreScript.UpdateScoreDisplay();
            }
        }

        else
        {
            if (SceneManager.GetActiveScene().name == "Regular Difficulty")
            {
                score = 0;
                Debug.Log("Wrong");
                StartCoroutine(Co_FlashTile(gridTile, Color.red, 0.25f));
                correctPosition.Clear();
                switchScene.LoadScene("Regular Difficulty Game Over");
                
            }
            
            else if (SceneManager.GetActiveScene().name == "Hard Difficulty")
            {
                score = 0;
                Debug.Log("Wrong");
                StartCoroutine(Co_FlashTile(gridTile, Color.red, 0.25f));
                correctPosition.Clear();
                switchScene.LoadScene("Hard Difficulty Game Over");
            }

            playerPatternIndex = 0;
        }
    }*/
    
    [ContextMenu("Next Pattern")]
    public void NextPattern()
    {
        playerPatternIndex = 0;
        score = 0;
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
            int randomRed = 3; 
            if (playerPatternIndex > 0)
            {
                randomRed = Random.Range(0, playerPatternIndex + 1);
                Debug.Log(randomRed);
            }
            GridTile tile = gridManager.GetTile(pos);

          
            yield return Co_FlashTile(tile, Color.green, 0.25f);
            yield return new WaitForSeconds(0.5f);
            
        }

        PatternPlaying = false;
        PatternPlayingText.text = "Click The Tiles in The Correct Pattern";
    }
    
    

    private IEnumerator Co_FlashTile(GridTile tile, Color color, float duration)
    {
        tile.SetColor(color);
        yield return new WaitForSeconds(duration);
        tile.SetColor(Color.white);
    }
    
    private IEnumerator Co_FlashSequence(GridTile tile, int randomRed, float duration)
    {
    
        for (int i = redFlashCheck; i < playerPatternIndex + 1; i++)
        {
            if (i == randomRed)
            {
                yield return Co_FlashTile(tile, Color.red, 0.25f);
            }
            else
            {
                yield return Co_FlashTile(tile, Color.green, 0.25f);
            }
        }
        
        redFlashCheck = playerPatternIndex; // Update redFlashCheck for the next sequence
    }
    IEnumerator Co_WaitRegularDifficulty()
    {
        yield return new WaitForSeconds(1.6f);
        switchScene.LoadScene("Regular Difficulty Game Over");
    }
    
    IEnumerator Co_WaitHardDifficulty()
    {
        yield return new WaitForSeconds(1.6f);
        switchScene.LoadScene("Hard Difficulty Game Over");
    }
    
}


    
    // private IEnumerator Co_FlashSequence(GridTile tile, int randomRed, float duration)
    // {
    //
    //     for (int i = redFlashCheck; i < playerPatternIndex + 1; redFlashCheck++)
    //     {
    //         
    //         if (i == randomRed)
    //         {
    //             tile.SetColor(Color.red);
    //             Debug.Log(playerPatternIndex);
    //         }
    //         else
    //         {
    //             tile.SetColor(Color.green);
    //             Debug.Log(playerPatternIndex);
    //                 
    //         }
    //     }
    //
    //     yield return new WaitForSeconds(duration);
    //     tile.ResetColor();
    //     yield return new WaitForSeconds(duration);
    // }
    
    
    // private IEnumerator Co_FlashSequence(GridTile tile, int randomRed, float duration)
    // {
    //     tile.ResetColor(); // Reset color before starting sequence
    //
    //     for (int i = 0; i < playerPatternIndex + 1; i++)
    //     {
    //         if (i == randomRed)
    //         {
    //             tile.SetColor(Color.red);
    //         }
    //         else
    //         {
    //             tile.SetColor(Color.green);
    //         }
    //         yield return new WaitForSeconds(duration);
    //         tile.ResetColor(); // Reset color after each flash
    //         yield return new WaitForSeconds(0.1f); // Add a small delay between flashes
    //     }

    