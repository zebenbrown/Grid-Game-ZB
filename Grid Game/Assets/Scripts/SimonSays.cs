using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSays : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;

    private List<Vector2Int> correctPosition  = new List<Vector2Int>();

    private bool PatternPlaying;
    private int playerPatternIndex;

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
            StartCoroutine(Co_FlashTile(gridTile, Color.green, 0.25f));
            playerPatternIndex++;
            if (playerPatternIndex == correctPosition.Count)
            {
                NextPattern();
            }
        }
        else
        {
            Debug.Log("Wrong");
            StartCoroutine(Co_FlashTile(gridTile, Color.red, 0.25f));
            correctPosition.Clear();
            NextPattern();
        }
    }
    
    [ContextMenu("Next Pattern")]
    public void NextPattern()
    {
        playerPatternIndex = 0;
        correctPosition.Add(new Vector2Int(Random.Range(0, gridManager.numColumns), Random.Range(0, gridManager.numRows)));
        StartCoroutine(Co_PlayPattern(correctPosition));
    }

    private IEnumerator Co_PlayPattern(List<Vector2Int> positions)
    {
        PatternPlaying = true;
        yield return new WaitForSeconds(1f);
        foreach (var pos in positions)
        {
            GridTile tile = gridManager.GetTile(pos);
            yield return Co_FlashTile(tile, Color.red, 0.25f);
            yield return new WaitForSeconds(0.5f);
        }

        PatternPlaying = false;
    }

    private IEnumerator Co_FlashTile(GridTile tile, Color color, float duration)
    {
        tile.SetColor(color);
        yield return new WaitForSeconds(duration);
        tile.ResetColor();
    }
}
