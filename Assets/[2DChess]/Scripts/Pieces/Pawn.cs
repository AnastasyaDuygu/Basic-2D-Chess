using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public override void HighlightSelectable(int x, int y, Tile[,] tileArray)
    {
        int rightx, leftx, yafter;
        //check color
        if (color == 1) //white
        {
            yafter = y + 1;
            rightx = x + 1; //check if there is piece infront it can eat
            leftx = x - 1;
        }
        else if (color == 2) //black
        {
            yafter = y - 1;
            rightx = x + 1;
            leftx = x - 1;
        }
        else
        {
            rightx = -1;
            leftx = -1;
            yafter = -1;
            Debug.LogError("invalid color" + color);
        }
        if (x < 8 && x >= 0 && yafter < 8 && yafter >= 0)
        {
            if (!isPieceInTile(tileArray[x, yafter]))
            {
                tileArray[x, yafter].SelectableHighlight();
            }
        }
        //for taking pieces
        if (rightx < 8 && rightx >= 0 && yafter < 8 && yafter >= 0)
        {
            if (isPieceInTile(tileArray[rightx, yafter]) && diffColor(tileArray[rightx,yafter], color)) 
            {
                Debug.Log("PIECE RIGHT");
                tileArray[rightx, yafter].SelectableHighlight();
            }
        }
        if (leftx < 8 && leftx >= 0 && yafter < 8 && yafter >= 0)
        {
            if (isPieceInTile(tileArray[leftx, yafter]) && diffColor(tileArray[leftx, yafter], color))
            {
                Debug.Log("PIECE LEFT");
                tileArray[leftx, yafter].SelectableHighlight();
            }
        }
        
        //if first move
        if (isFirstMove && !isPieceInTile(tileArray[x,yafter]))
        {
            if (color == 1)
                yafter++; //white
            else yafter--; //black
            
            if (x < 8 && x >= 0 && yafter < 8 && yafter >= 0)
            {
                if (!isPieceInTile(tileArray[x, yafter]))
                {
                    tileArray[x, yafter].SelectableHighlight();
                }
            }
        }
    }
}
