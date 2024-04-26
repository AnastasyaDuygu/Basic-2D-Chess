using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public override void HighlightSelectable(int x, int y, Tile[,] tileArray)
    {
        Debug.Log("Selectable Bishop");
    }
}