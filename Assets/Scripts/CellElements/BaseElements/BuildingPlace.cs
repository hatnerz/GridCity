using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingPlace : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private SpriteRenderer buildingSpriteRenderer;

    public Vector2Int GridPosition { get; set; }

    public Building Building { get; set; }

    public BuildingData BuildingData { get; set; }

    public delegate void BuildingPlaceHandler(BuildingPlace buildingPlace);

    public static event BuildingPlaceHandler OnMouseEnter;
    public static event BuildingPlaceHandler OnMouseExit;
    public static event BuildingPlaceHandler OnMouseClick;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseEnter?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnMouseExit?.Invoke(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnMouseClick?.Invoke(this);
    }

    public void VisualizeBuilding()
    {
        buildingSpriteRenderer.sprite = BuildingData.BuildingSprite;
        buildingSpriteRenderer.transform.localScale = BuildingData.BuildingSpriteScale;
        buildingSpriteRenderer.transform.localPosition = BuildingData.BuildingSpriteOffset;
        buildingSpriteRenderer.sortingOrder = (GridPosition.x * 2 - GridPosition.y * 2) + 3;
    }
}
