using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    public TileManager _tileManager;
    
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
        if (currentlySelectedTile == null) {
            StartCoroutine(SelectTile());

            //if selected tile has a piece in it highlight possible selectable tiles
            //PieceSelectedEvent.Invoke(); 
            //if clicked on selectable tile piece moved event is triggered
        }else {
            DeselectTile();
        }
    }
    IEnumerator SelectTile()
    {
        _tileManager.DeselectAllTiles(); //deselect all tiles before selecting new tile
        while (!_tileManager.isDone)
            yield return null;

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
    public void DeselectSelected()
    {
        selected.SetActive(false);
    }
}