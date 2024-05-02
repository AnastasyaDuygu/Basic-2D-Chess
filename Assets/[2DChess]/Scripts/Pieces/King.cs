using UnityEngine;

public class King : Piece
{
    public override void HighlightSelectable(int x, int y, Tile[,] tileArray)
    {
        //Debug.Log("Selectable King");
        
        int[] X = { -1, 1, -1, 1, 0, 0, -1, 1 };
        int[] Y = { 1, 1, -1, -1, 1, -1, 0, 0 };
        for (int i = 0; i < 8; i++)
        {
            int xafter = x + X[i];
            int yafter = y + Y[i];

            if (xafter < 8 && xafter >= 0 && yafter < 8 && yafter >= 0)
            {
                if (!isPieceInTile(tileArray[xafter, yafter]))
                    tileArray[xafter, yafter].SelectableHighlight();
                else if (diffColor(tileArray[xafter, yafter], color))
                {
                    tileArray[xafter, yafter].SelectableHighlight();
                    if (tileArray[xafter, yafter].holdedPiece.GetType() == typeof(King))
                    {
                        OnCheck();
                    }
                }
            }
        }
    }
}