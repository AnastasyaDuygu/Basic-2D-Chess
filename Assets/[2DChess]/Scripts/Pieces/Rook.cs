using UnityEngine;

public class Rook : Piece
{
    public override void HighlightSelectable(int x, int y, Tile[,] tileArray)
    {
        //Debug.Log("Selectable Rook");
        
        int[] X = { 1, 0, -1, 0 };
        int[] Y = { 0, -1, -0, 1 };

        for (int i = 0; i < 4; i++)
        {
            int xafter = x;
            int yafter = y;

            for (int j = 0; j < 8; j++)
            {
                xafter += X[i];
                yafter += Y[i];
                
                if (xafter < 8 && xafter >= 0 && yafter < 8 && yafter >= 0)
                {
                    if (!isPieceInTile(tileArray[xafter, yafter]))
                        tileArray[xafter, yafter].SelectableHighlight();
                    else if (diffColor(tileArray[xafter, yafter], color)){
                        tileArray[xafter, yafter].SelectableHighlight();
                        break;
                    } else break;
                }
            }
        }
    }
}