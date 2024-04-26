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
    public Tile currentlySelectedTile = null;

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
            StartCoroutine(SelectTileCoroutine());

            //if selected tile has a piece in it highlight possible selectable tiles
            if (holdedPiece != null)
                holdedPiece.HighlightSelectable(x, y, _tileManager.tilesArray);

            if (isSelectable && !SameColor()) //if clicked on selectable tile piece moved event is triggered
                _pieceMovement.MovePiece();
            
        } else DeselectTile();
        
    }
    IEnumerator SelectTileCoroutine()
    {
        _tileManager.isDone = false;
        _tileManager.DeselectAllSelected(); //deselect all tiles before selecting new tile
        while (!_tileManager.isDone)
            yield return null;
        
        SelectTile();
    }

    private void SelectTile()
    {
        isSelected = true;
        selected.SetActive(true);
        currentlySelectedTile = this;
    }
    private void DeselectTile()
    {
        isSelected = false;
        selected.SetActive(false);
        currentlySelectedTile = null;
        _tileManager.DeselectAllSelectable();
    }
    public void DeselectSelected()
    {
        selected.SetActive(false);
        isSelected = false;
    }
    public void DeselectSelectable()
    {
        selectable.SetActive(false);
        isSelectable = false;
    }
    public void SelectableHighlight()
    {
        isSelectable = true;
        selectable.SetActive(true);
    }
    private bool SameColor()
    {
        if(holdedPiece != null && holdedPiece.color == currentlySelectedTile.holdedPiece.color)
        {
            return true;
        }
        return false;
    }
}