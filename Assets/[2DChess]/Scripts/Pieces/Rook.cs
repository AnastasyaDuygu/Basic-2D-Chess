using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public override void HighlightSelectable(int x, int y, Tile[,] tileArray)
    {
        Debug.Log("Selectable Rook");
    }
}