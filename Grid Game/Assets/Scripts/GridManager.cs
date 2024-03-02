using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public event Action<GridTile> TileSelected;
    
    public int numRows = 5;

    public int numColumns = 6;

    [SerializeField] private GridTile tilePrefab;
    [SerializeField] private TextMeshProUGUI text;
    private GridTile[] tiles;

    public float padding = 0.1f;

    private void Awake()
    {
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
}
