using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    public TileManager _tileManager;
    public PieceMovement _pieceMovement;
    
    public int x = -1;
    public int y = -1;
    
    public Piece holdedPiece;
    
    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [SerializeField] private GameObject hover,selected,selectable;

    public bool isSelected = false;
    public bool isSelectable = false;

    private void OnEnable()
    {
        _tileManager = FindObjectOfType<TileManager>();
        _pieceMovement = FindObjectOfType<PieceMovement>();
    }
    public void Init(bool isOffset)
    {
        spriteRenderer.color = isOffset ? offsetColor : baseColor;
    }
    private void OnMouseEnter()
    {
        if (!isSelected)
            hover.SetActive(true);
    }
    private void OnMouseExit()
    {
        hover.SetActive(false);
    }
    private void OnMouseDown()
    {
        if (!isSelected)
        {
            //deselect tile before selecting new tile
            if (_tileManager.currentlySelectedTile != null) _tileManager.currentlySelectedTile.DeselectTile();
            
            if (isSelectable && _tileManager.currentlySelectedTile.holdedPiece != null) //if clicked on selectable tile piece moved event is triggered
                _pieceMovement.MovePiece(_tileManager.currentlySelectedTile, this, _tileManager);
            
            SelectTile();
            
        } else DeselectTile();
    }
    private void SelectTile()
    {
        isSelected = true;
        selected.SetActive(true);
        _tileManager.currentlySelectedTile = this;
        //if selected tile has a piece in it highlight possible selectable tiles
        if (holdedPiece != null)
        {
            _tileManager.IsSelectableAllFalse(); //deselect all isSelectable when a tile that is not selectable is clicked
            
            if(IsTurn()) //check turn
                holdedPiece.HighlightSelectable(x, y, _tileManager.tilesArray);
        }
            
    }
    private void DeselectTile()
    {
        isSelected = false;
        selected.SetActive(false);
        //_tileManager.currentlySelectedTile = null;
        
        //deselect all tiles selected & selectable
        _tileManager.DeselectAllSelectable();
    }
    public void DeselectSelectable()
    {
        selectable.SetActive(false);
        //isSelectable = false;
    }

    public void DeselectEverything()
    {
        selectable.SetActive(false);
        isSelectable = false;
        selected.SetActive(false);
        isSelected = false;
    }
    public void SelectableHighlight()
    {
        isSelectable = true;
        selectable.SetActive(true);
    }
    private bool SameColorPiece()
    {
        if(holdedPiece != null && holdedPiece.color == _tileManager.currentlySelectedTile.holdedPiece.color)
        {
            return true;
        }
        return false;
    }
    public bool IsTurn()
    {
        if (holdedPiece.color == 1 && _tileManager.gameTurn == false) //white
            return true;
        else if (holdedPiece.color == 2 && _tileManager.gameTurn == true) //black
            return true;
        else return false;
    }
}