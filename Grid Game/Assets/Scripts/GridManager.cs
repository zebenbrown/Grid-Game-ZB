using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public event Action<GridTile> TileSelected;
    
    public int numRows = 5;

    public int numColumns = 6;

    [SerializeField] private GridTile tilePrefab;
    [SerializeField] private TextMeshProUGUI text;
    //[SerializeField] private float DoJumpX = -3.6f;
    //[SerializeField] private float DoJumpY = -3.95f;
    //[SerializeField] private float DoJumpPower = 5f;
    //[SerializeField] private float DoJumpDuration = 3f;
    [SerializeField] private float FadeInDuration;
    
    private GridTile[] tiles;
    private FadeScript fadeScript;
    public float padding = 0.1f;
    public CanvasGroup canvasGroup;
    private void Awake()
    {
        DOTween.Init();
        CanvasFadeIn();
        InitializeGrid();
        
    }

    public void InitializeGrid()
    {
        tiles = new GridTile[numRows * numColumns];
        
        for (int y = 0; y < numRows; y++)
        {
            for (int x = 0; x < numColumns; x++)
            {
                
                GridTile tile = Instantiate(tilePrefab, transform);
                
                Vector2 tilePos = new Vector2(x + (padding * x), y + (padding * y));
                tile.transform.localPosition = tilePos;
                tile.name = $"Tile_{x}_{y}";
                tile.gridManager = this;
                tile.gridCoordinates = new Vector2Int(x, y);
                tiles[y * numColumns + x] = tile;
                
                SpriteRenderer spriteRenderer = tile.GetComponent<SpriteRenderer>();
                Color c = spriteRenderer.color;
                c.a = 0f;
                spriteRenderer.color = c;

                // Fade in the tile
                spriteRenderer.DOFade(1f, FadeInDuration);
            }
        }
    }

    public void OnTileHoverEnter(GridTile gridTile)
    {
        text.text = gridTile.gridCoordinates.ToString();
        
    }

    public void OnTileHoverExit(GridTile gridTile)
    {
        text.text = "(-,-)";
    }
    
    public void OnTileSelected(GridTile gridTile)
    {
        TileSelected?.Invoke(gridTile);
    }

    public GridTile GetTile(Vector2Int pos)
    {
        if (pos.x < 0 || pos.x >= numColumns || pos.y < 0 || pos.y >= numRows)
        {
            Debug.LogError($"Invalid Coordinate{pos}");
            return null;
        }
        return tiles[pos.y * numColumns + pos.x];
    }

    public void GridFade()
    {
        foreach (GridTile tile in tiles)
        {
            SpriteRenderer spriteRenderer = tile.GetComponent<SpriteRenderer>();
            Color c = spriteRenderer.color;
            c.a = 1f;
            spriteRenderer.color = Color.red;
            spriteRenderer.DOFade(0, 1.5f);
        }
    }

    void CanvasFadeIn()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1, FadeInDuration);
    }
    
}
