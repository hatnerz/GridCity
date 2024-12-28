using UnityEngine;
using static CardManager;

public class GridManager : MonoBehaviour, IGridState
{
    [SerializeField] private int sizeX = 10;
    [SerializeField] private int sizeY = 10;
    [SerializeField] private GridVisualizer gridVisualizer;
    [SerializeField] private CardManager cardManager;
    [SerializeField] private LevelData levelData;

    public int SizeX { get { return sizeX; } }
    public int SizeY { get { return sizeY; } }

    public CellElement[,] GridElements { get; private set; }
    public GameObject[,] BuildingPlaces { get; private set; }

    public GameObject CurrentHoveringBuildingPlace { get; private set; }

    public event BuildingEventHandler OnBuildingPlaced;

    public delegate void BuildingEventHandler(BuildingPlace placedBuilding);

    private void Start()
    {
        if(levelData != null)
        {
            InitializeLevelGrid(levelData);
        }
    }

    public void InitializeLevelGrid(LevelData levelData)
    {
        if (gridVisualizer == null)
        {
            throw new MissingReferenceException("GridVisualizer is not set in GridManager");
        }

        var gridSize = levelData.GridSize;
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

        Debug.Log($"Building placed on {buildingPlace.GridPosition} ");

        OnBuildingPlaced?.Invoke(buildingPlace);
    }
}

