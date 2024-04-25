using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    //[SerializeField] private TileManager _tileManager;
    public UnityEvent PieceSelectedEvent;
    public Piece holdedPiece;
    
    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [SerializeField] private GameObject hover,selected,selectable;

    public bool isSelected = false;
    public bool isSelectable = false;
    public Tile currentlySelectedTile = null;

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
        if (currentlySelectedTile == null)
        {
            //DeselectAllTiles();
            SelectTile(); // Select a tile after all tiles are deselected
            
            //if selected tile has a piece in it highlight possible selectable tiles
            //PieceSelectedEvent.Invoke(); 
            //if clicked on selectable tile piece moved event is triggered
        }else {
            DeselectTile();
        }
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
    }
    public void Deselect()
    {
        selected.SetActive(false);
    }
}