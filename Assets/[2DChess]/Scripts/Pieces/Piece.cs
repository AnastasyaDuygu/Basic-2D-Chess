using UnityEngine;

public class Piece : MonoBehaviour
{
    public int color = 0; //1:white 2:black
    public bool isFirstMove = true; //for pawns, set false after first move

    public virtual void HighlightSelectable(int x, int y, Tile[,] tileArray) { }
    
    public bool isPieceInTile(Tile tile) 
    {
        return tile.holdedPiece != null;
    }
    public bool diffColor(Tile tile, int color)
    {
        if (tile.holdedPiece.color != color) return true;
        return false;
    }
}
