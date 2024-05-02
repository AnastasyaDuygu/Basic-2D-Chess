using UnityEngine;
using UnityEngine.Events;

public class PieceMovement : MonoBehaviour
{
    public bool winner; //white = false, black = true
    public UnityEvent GameEndEvent;
    public void MovePiece(Tile currentlySelectedTile, Tile clickedTile, TileManager tileManager)
    {
        //Debug.Log("Piece Moved");
        
        //deselect all highlights & isSelectable/isSelected
        if (currentlySelectedTile.holdedPiece.isFirstMove == true)
        {
            currentlySelectedTile.holdedPiece.isFirstMove = false;
        }

        if (clickedTile.holdedPiece != null)
        {
            if (clickedTile.holdedPiece.GetType() == typeof(King))
            {
                if (clickedTile.holdedPiece.color == 1)
                    winner = true; // black
                else winner = false;
                GameEndEvent.Invoke(); //enable end game canvas
            }
            Destroy(clickedTile.holdedPiece.gameObject);
        }
            
        
        Piece piece = currentlySelectedTile.holdedPiece;
        //change transform of piece to the transform of clicked tile
        piece.transform.position = clickedTile.transform.position;
        piece.transform.parent = clickedTile.transform; //change parent of piece
        
        clickedTile.holdedPiece = piece;
        currentlySelectedTile.holdedPiece = null;
        
        //switch turn
        clickedTile.holdedPiece.HighlightSelectable(clickedTile.x, clickedTile.y, tileManager.tilesArray); //needed for detecting check
        tileManager.SwitchTurn();
    }
}
