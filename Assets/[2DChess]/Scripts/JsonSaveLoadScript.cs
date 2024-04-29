using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonSaveLoadScript : MonoBehaviour
{
    public Tile[,] tilesArray = new Tile[8, 8];
    public int[,] startArray = new int[8, 8]; 
    public float elapsedTime;
    public bool gameTurn;

    public UIManager _UIManager;
    public TileManager _TileManager;
    private void Awake()
    {
        _UIManager = FindObjectOfType<UIManager>();
        _TileManager = FindObjectOfType<TileManager>();
    }
    public void SaveToJson()
    {
        //Set Data
        Array.Copy(_TileManager.tilesArray, tilesArray, tilesArray.Length); //
        elapsedTime = _UIManager.elapsedTime;
        gameTurn = _TileManager.gameTurn;
        
        GameData data = new GameData();
        data.startArray = convertArrayToString();
        data.elapsedTime = elapsedTime;
        data.gameTurn = gameTurn;

        string output = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/[2DChess]/Data/GameDataFile.json", output);
        Debug.Log(output);
    }
    public void LoadFromJson()
    {
        if(CheckIfFileExists())
        {
            string jsonData = File.ReadAllText(Application.dataPath + "/[2DChess]/Data/GameDataFile.json");
            GameData data = JsonUtility.FromJson<GameData>(jsonData);
            //
            _UIManager.elapsedTime = data.elapsedTime;
            _TileManager.gameTurn = data.gameTurn;
            _TileManager.startArray = convertJsonStringToArray(data.startArray);
        }
        else Debug.LogWarning("NO SAVED GAME");
    }
    private string convertArrayToString()
    {
        string pieceName;
        int pieceCode = -1;
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                pieceName = "";
                if(tilesArray[x, y].holdedPiece != null)
                    pieceName = tilesArray[x, y].holdedPiece.name.Remove(tilesArray[x, y].holdedPiece.name.Length - 7);
                pieceCode = findPieceCode(pieceName);
                startArray[y, x] = pieceCode;
            }
        }
        var jsonString = JsonConvert.SerializeObject(startArray);
        return jsonString;
    }
    private int findPieceCode(string pieceName)
    {
        int num = 0;
        if (pieceName == "") 
            return num;
        switch (pieceName)
        {
            case "white_pawn": num = 11; break;
            case "white_rook": num = 12; break;
            case "white_knight": num = 13;break;
            case "white_bishop": num = 14; break;
            case "white_queen": num = 15; break;
            case "white_king": num = 16; break;
            case "black_pawn": num = 21; break;
            case "black_rook": num = 22; break;
            case "black_knight": num = 23; break;
            case "black_bishop": num = 24; break;
            case "black_queen": num = 25; break;
            case "black_king": num = 26; break;
        }
        return num;
    }
    private int[,] convertJsonStringToArray(string json)
    {
        var tiles = JsonConvert.DeserializeObject<int[,]>(json);
        return tiles;
    }

    public bool CheckIfFileExists()
    {
        if (File.Exists(Application.dataPath + "/[2DChess]/Data/GameDataFile.json"))
        {
            return true;
        }
        return false;
    }
}
