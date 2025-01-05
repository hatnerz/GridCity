using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class BuildingPlace : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private SpriteRenderer buildingSpriteRenderer;
    [SerializeField] private GameObject highlightObject;
    [SerializeField] private Color highlightColor = Color.yellow;
    [SerializeField] private Color cannotBuildColor = Color.yellow;
    [SerializeField] private Color canBuildColor = Color.green;

    public Vector2Int GridPosition { get; set; }

    public Building Building { get; set; }

    public BuildingData BuildingData { get; set; }

    public Color originalColor;
    private GridManager gridManager;

    public delegate void BuildingPlaceHandler(BuildingPlace buildingPlace);

    public static event BuildingPlaceHandler OnMouseEnter;
    public static event BuildingPlaceHandler OnMouseExit;
    public static event BuildingPlaceHandler OnMouseClick;

    public void Initialize(GridManager manager)
    {
        gridManager = manager;
    }

    public void Awake()
    {
        //if (buildingSpriteRenderer != null)
        //    originalColor = buildingSpriteRenderer.color;

        if (highlightObject != null)
            highlightObject.SetActive(false);

        cannotBuildColor.a = 0.3f;
        canBuildColor.a = 0.3f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseEnter?.Invoke(this);
        SetHighlight(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnMouseExit?.Invoke(this);
        SetHighlight(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnMouseClick?.Invoke(this);
    }

    public void SetHighlight(bool highlight)
    {
        //if (buildingSpriteRenderer != null)
        //    buildingSpriteRenderer.color = highlight ? highlightColor : originalColor;

        if (Building != null)
        {
            highlight = false;
        }


        if (highlightObject != null)
        {
            highlightObject.SetActive(highlight);

            var highlightRenderer = highlightObject.GetComponent<SpriteRenderer>();
            if (highlightRenderer != null)
            {
                highlightRenderer.color = gridManager.HasNeighbors(GridPosition) || gridManager.isEmptyGrid ? canBuildColor : cannotBuildColor;
                highlightRenderer.sortingOrder = 100;
            }
        }
    }

    public void VisualizeBuilding()
    {
        buildingSpriteRenderer.sprite = BuildingData.BuildingSprite;
        buildingSpriteRenderer.transform.localScale = BuildingData.BuildingSpriteScale;
        buildingSpriteRenderer.transform.localPosition = BuildingData.BuildingSpriteOffset;
        buildingSpriteRenderer.sortingOrder = (GridPosition.x * 2 - GridPosition.y * 2) + 3;
    }
}
