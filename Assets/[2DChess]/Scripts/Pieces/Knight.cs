using UnityEngine;

public class Knight : Piece
{
    public override void HighlightSelectable(int x, int y, Tile[,] tileArray)
    {
        //Debug.Log("Selectable Knight");
        
        int[] X = { 2, 1, -1, -2, -2, -1, 1, 2 };
        int[] Y = { 1, 2, 2, 1, -1, -2, -2, -1 };
        for (int i = 0; i < 8; i++)
        {
            int xafter = x + X[i];
            int yafter = y + Y[i];
            
            if (xafter < 8 && xafter >= 0 && yafter < 8 && yafter >= 0)
            {
                if (!isPieceInTile(tileArray[xafter, yafter]) || diffColor(tileArray[xafter, yafter], color))
                {
                    tileArray[xafter, yafter].SelectableHighlight();
                }
            }
        }
    }
}