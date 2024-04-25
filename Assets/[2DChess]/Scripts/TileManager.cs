using System;
using UnityEngine;

public class TileManager : MonoBehaviour
{
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
    
    //private Vector3 startPoint = new Vector3(-1.12f,-1.12f,0);
    //private static float incrementAmount = 0.32f;
    
    void OnEnable()
    {
        if (isSavedGame)
        {
            //load start array from json
        }
    }
    public void GenerateGrid()
    {
        int i = 0;
        foreach(Transform child in transform)
        {
            int x = i % 8;
            int y = i / 8;
            var currentTile = child.GetComponent<Tile>();
            var pos = currentTile.transform.position;
            
            currentTile = SpawnPieceForTile(pos, currentTile, x, y);
            
            
            tilesArray[x, y] = currentTile;
            Debug.Log("tiles array [" + i%8 + ", "+ i/8+" ] : " + tilesArray[i%8, i/8]); //works !!
            i++;
        }
    }

    private Tile SpawnPieceForTile(Vector3 pos, Tile currentTile, int x, int y)
    {
        int startPieceCode = startArray[y, x]; //it needs to be flipped because of the start array
        if (startPieceCode == 0) return currentTile;
        Piece piece = null;
        if (startPieceCode / 10 == 1) //white
            piece = piecePrefabsW[startPieceCode % 10 - 1];
        else if (startPieceCode / 10 == 2) //black
            piece = piecePrefabsB[startPieceCode % 10 - 1];

        if (piece != null)
        {
            var spawnedPiece = Instantiate(piece, pos, Quaternion.identity, currentTile.transform);
            spawnedPiece.transform.localScale = new Vector3(4, 4, 1);
            spawnedPiece.isFirstMove = false;
            currentTile.holdedPiece = spawnedPiece;
        }
        return currentTile;
        
    }
    public void DeselectAllTiles() //does not work
    {
        foreach (var tile in tilesArray)
        {
            tile.Deselect();
        }
    }
}
