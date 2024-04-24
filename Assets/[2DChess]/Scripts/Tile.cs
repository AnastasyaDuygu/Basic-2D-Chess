using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [SerializeField] private GameObject hover,selected,selectable;

    public bool isSelected = false;
    public void Init(bool isOffset)
    {
        spriteRenderer.color = isOffset ? offsetColor : baseColor;
    }
    private void OnMouseEnter()
    {
        if (!isSelected)
        {
            hover.SetActive(true);
        }
    }
    private void OnMouseExit()
    {
        hover.SetActive(false);
    }
}