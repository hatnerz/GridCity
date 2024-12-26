using UnityEngine;

public class GridManager : MonoBehaviour, IGridState
{
    [SerializeField] private int sizeX = 10;
    [SerializeField] private int sizeY = 10;
    [SerializeField] private GridVisualizer gridVisualizer;
    [SerializeField] private CardManager cardManager;

    public int SizeX { get { return sizeX; } }
    public int SizeY { get { return sizeY; } }

    public CellElement[,] GridElements { get; private set; }
    public GameObject[,] BuildingPlaces { get; private set; }

    public GameObject CurrentHoveringBuildingPlace { get; private set; } 

    void Start()
    {
        if(gridVisualizer == null)
        {
            throw new MissingReferenceException("GridVisualizer is not set in GridManager");
        }

        var gridSize = new Vector2Int(sizeX, sizeY);
        gridVisualizer.VisualizeGrid(gridSize);
        BuildingPlaces = gridVisualizer.CreateAllBuildingPlaces(gridSize);
    }

    private void OnEnable()
    {
        BuildingPlace.OnMouseEnter += HandleBuildingPlaceEnter;
        BuildingPlace.OnMouseExit += HandleBuildingPlaceExit;
        BuildingPlace.OnMouseClick += HandleBuildingPlaceClick;
    }

    private void OnDisable()
    {
        BuildingPlace.OnMouseEnter -= HandleBuildingPlaceEnter;
        BuildingPlace.OnMouseExit -= HandleBuildingPlaceExit;
        BuildingPlace.OnMouseClick -= HandleBuildingPlaceClick;
    }

    private void HandleBuildingPlaceEnter(BuildingPlace buildingPlace)
    {
        // Debug.Log($"Pointer moved to building position {buildingPlace.GridPosition}");
        CurrentHoveringBuildingPlace = buildingPlace.gameObject;
    }

    private void HandleBuildingPlaceExit(BuildingPlace buildingPlace)
    {
        // Debug.Log($"Pointer moved from building position  {buildingPlace.GridPosition}");
        CurrentHoveringBuildingPlace = null;
    }

    private void HandleBuildingPlaceClick(BuildingPlace buildingPlace)
    {
        var buildingToBuild = cardManager.TryPlayActiveCard();

        if (buildingToBuild == null)
        {
            Debug.Log("Building not picked");
            return;
        }

        var buildingData = ResourceManager.Instance.BuildingDataDictionary[buildingToBuild.BuildingType];

        buildingPlace.Building = buildingToBuild;
        buildingPlace.BuildingData = buildingData;
        buildingPlace.VisualizeBuilding();

        Debug.Log($"Try to place building on {buildingPlace.GridPosition} ");
    }
}