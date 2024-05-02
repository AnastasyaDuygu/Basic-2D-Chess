using UnityEngine;

public class Queen : Piece
{
    public override void HighlightSelectable(int x, int y, Tile[,] tileArray)
    {
        //Debug.Log("Selectable Queen");
        
        //Rook+Bishop Moves
        int[] X = { 1, 0, -1, 0, -1, 1, -1, 1 };
        int[] Y = { 0, -1, -0, 1, 1, 1, -1, -1 };
        for (int i = 0; i < 8; i++)
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
                        if (tileArray[xafter, yafter].holdedPiece.GetType() == typeof(King))
                        {
                            OnCheck();
                        }
                        break;
                    } else break;
                }
            }
        }
    }
}