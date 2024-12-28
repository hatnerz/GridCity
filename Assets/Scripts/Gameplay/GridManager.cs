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
    public GameObject[,] BuildingPlacesObjects { get; private set; }

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
        BuildingPlacesObjects = gridVisualizer.CreateAllBuildingPlaces(gridSize);
        GridElements = new CellElement[gridSize.x, gridSize.y];

        for(int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                GridElements[x, y] = new Ground("Land", GroundType.Land);
            }
        }
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
        buildingToBuild.GridPosition = new Vector2Int(buildingPlace.GridPosition.x, buildingPlace.GridPosition.y);

        if (buildingToBuild == null)
        {
            Debug.Log("Building not picked");
            return;
        }

        var buildingData = ResourceManager.Instance.BuildingDataDictionary[buildingToBuild.BuildingType];

        buildingPlace.Building = buildingToBuild;
        buildingPlace.BuildingData = buildingData;
        buildingPlace.VisualizeBuilding();
        GridElements[buildingPlace.GridPosition.x, buildingPlace.GridPosition.y] = buildingPlace.Building;

        Debug.Log($"Building placed on {buildingPlace.GridPosition} ");
        OnBuildingPlaced?.Invoke(buildingPlace);
    }
}

