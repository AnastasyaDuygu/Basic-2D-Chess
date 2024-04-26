using System;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tile tilePrefab;

    public bool isSavedGame = false;

    public Piece[] piecePrefabsW = new Piece[6];
    public Piece[] piecePrefabsB = new Piece[6];
    public int[,] startArray = { 
        {12, 13, 14, 15, 16, 14, 13, 12},
        {11, 11, 11, 11, 11, 11, 11, 11},
        {0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 },
        {0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 },
        {0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 },
        {0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 },
        {21, 21, 21, 21, 21, 21, 21, 21},
        {22, 23, 24, 25, 26, 24, 23, 22}   
    };
    public Tile[,] tilesArray = new Tile[8,8];
    
    private Vector3 startPoint = new Vector3(-1.12f,-1.12f,0);
    private static float incrementAmount = 0.32f;
    
    void OnEnable()
    {
        if (isSavedGame)
        {
            //load start array from json
        }
    }
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
                
                SpawnPieceForTile(pos, spawnedTile, x, y);
                
                tilesArray[x,y] = spawnedTile; //add to tile array
            }
        }
    }

    private void SpawnPieceForTile(Vector3 pos, Tile spawnedTile, int x, int y)
    {
        int startPieceCode = startArray[y, x];
        if (startPieceCode == 0) return;
        Piece piece = null;
        if (startPieceCode / 10 == 1) //white
            piece = piecePrefabsW[startPieceCode % 10 - 1];
        else if (startPieceCode / 10 == 2) //black
            piece = piecePrefabsB[startPieceCode % 10 - 1];

        if (piece != null)
        {
            var spawnedPiece = Instantiate(piece, pos, Quaternion.identity, spawnedTile.transform);
            spawnedPiece.transform.localScale = new Vector3(4, 4, 1);
            spawnedPiece.isFirstMove = false;
            spawnedTile.holdedPiece = spawnedPiece;
        }
        
    }
    public void DeselectAllTiles()
    {
        foreach (var tile in tilesArray)
        {
            tile.Deselect();
        }
    }
}
