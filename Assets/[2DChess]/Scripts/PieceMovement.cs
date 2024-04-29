using UnityEngine;

public class PieceMovement : MonoBehaviour
{
    public void MovePiece(Tile currentlySelectedTile, Tile clickedTile, TileManager tileManager)
    {
        Debug.Log("Piece Moved");
        
        //deselect all highlights & isSelectable/isSelected
        if (currentlySelectedTile.holdedPiece.isFirstMove == true)
        {
            currentlySelectedTile.holdedPiece.isFirstMove = false;
        }
        
        Piece piece = currentlySelectedTile.holdedPiece;
        //change transform of piece to the transform of clicked tile
        piece.transform.position = clickedTile.transform.position;
        piece.transform.parent = clickedTile.transform; //change parent of piece
        
        clickedTile.holdedPiece = piece;
        currentlySelectedTile.holdedPiece = null;
        
        //switch turn
        tileManager.SwitchTurn();
    }
}
