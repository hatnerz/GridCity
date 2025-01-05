using System.Collections;
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

    public IEnumerator AnimateBuilding(float duration = 0.5f, float g = 40f)
    {
        Transform buildingTransform = buildingSpriteRenderer.transform;

        // Початкова позиція (згори)
        Vector3 startPosition = new Vector3(buildingTransform.position.x, buildingTransform.position.y + 6f, buildingTransform.position.z);

        // Кінцева позиція (місце на клітинці)
        Vector3 endPosition = buildingTransform.position;

        buildingTransform.position = startPosition; // Установлюємо початкову позицію

        float elapsedTime = 0f;
        float velocity = 2f; // Початкова швидкість

        // Анімація падіння
        while (elapsedTime < duration)
        {
            // Розраховуємо нову позицію з урахуванням гравітації
            velocity += g * Time.deltaTime;
            float deltaY = velocity * Time.deltaTime;
            buildingTransform.position = new Vector3(
                startPosition.x,
                Mathf.Max(buildingTransform.position.y - deltaY, endPosition.y), // Забезпечуємо, що не пройдемо кінцеву точку
                startPosition.z
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Установлюємо точну кінцеву позицію, щоб уникнути неточностей
        buildingTransform.position = endPosition;

        PlayParticles();
    }

    public void PlayParticles()
    {
        var particleSystem = GetComponentInChildren<ParticleSystem>();
        if (particleSystem != null)
        {
            particleSystem.Play();
        }
    }

}
