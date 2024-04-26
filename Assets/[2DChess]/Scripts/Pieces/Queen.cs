using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override void HighlightSelectable(int x, int y, Tile[,] tileArray)
    {
        Debug.Log("Selectable Queen");
    }
}