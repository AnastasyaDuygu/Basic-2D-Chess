using System;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tile tilePrefab;

    public Tile[,] tilesArray = new Tile[8,8];
    
    private Vector3 startPoint = new Vector3(-1.12f,-1.12f,0);
    private static float incrementAmount = 0.32f;
    public void GenerateGrid()
    {
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                var pos = startPoint;
                pos.x += incrementAmount * x;
                pos.y += incrementAmount * y;
                
                var spawnedTile = Instantiate(tilePrefab, pos, Quaternion.identity, transform);
                spawnedTile.name = $"Tile {x} {y}";
                var isOffset = Math.Abs((x + y) % 2) == 1;
                spawnedTile.Init(isOffset);
                
                tilesArray[x,y] = spawnedTile; //add to tile array
            }
        }
    }
}
