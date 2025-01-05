using Assets.Scripts.Helpers;
using Unity.VisualScripting;
using UnityEngine;
using static CardManager;

public class GridManager : MonoBehaviour, IGridState
{
    [SerializeField] private GridVisualizer gridVisualizer;
    [SerializeField] private CardManager cardManager;
    [SerializeField] private LevelData levelData;

    public int SizeX { get { return GridElements.GetLength(0); } }
    public int SizeY { get { return GridElements.GetLength(1); } }

    public bool isEmptyGrid { get; private set; }
    private BuildingHighlighter groundHighlighter { get; set; }

    public CellElement[,] GridElements { get; private set; }
    public GameObject[,] BuildingPlacesObjects { get; private set; }

    public GameObject CurrentHoveringBuildingPlace { get; private set; }

    public event BuildingEventHandler OnBuildingPlaced;

    public delegate void BuildingEventHandler(BuildingPlace placedBuilding);

    private void Start()
    {
        if (levelData != null)
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
        isEmptyGrid = true;

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                GridElements[x, y] = new Ground("Land", GroundType.Land);

                var buildingPlace = BuildingPlacesObjects[x, y].GetComponent<BuildingPlace>();
                if (buildingPlace != null)
                {
                    buildingPlace.Initialize(this); 
                }
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
        //Debug.Log($"Pointer moved to building position {buildingPlace.GridPosition}");
        CurrentHoveringBuildingPlace = buildingPlace.gameObject;
        buildingPlace.SetHighlight(true);
    }

    private void HandleBuildingPlaceExit(BuildingPlace buildingPlace)
    {
        // Debug.Log($"Pointer moved from building position  {buildingPlace.GridPosition}");
        CurrentHoveringBuildingPlace = null;
        buildingPlace.SetHighlight(false);
    }

    private void HandleBuildingPlaceClick(BuildingPlace buildingPlace)
    {
        if (!AllowedToBuild(buildingPlace))
            return;

        var buildingToBuild = cardManager.TryPlayActiveCard();


        if (buildingToBuild == null)
        {
            Debug.Log("Building not picked");
            return;
        }

        buildingToBuild.GridPosition = new Vector2Int(buildingPlace.GridPosition.x, buildingPlace.GridPosition.y);

        var buildingData = ResourceManager.Instance.BuildingDataDictionary[buildingToBuild.BuildingType];

        buildingPlace.Building = buildingToBuild;
        buildingPlace.BuildingData = buildingData;
        buildingPlace.VisualizeBuilding();
        GridElements[buildingPlace.GridPosition.x, buildingPlace.GridPosition.y] = buildingPlace.Building;

        buildingPlace.SetHighlight(false);

        OnBuildingPlaced?.Invoke(buildingPlace);

        isEmptyGrid = false;
    }

    private bool AllowedToBuild(BuildingPlace buildingPlace)
    {
        if (GridElements[buildingPlace.GridPosition.x, buildingPlace.GridPosition.y] is not Ground)
            return false;

        var neighbors = GridElementsHelper.GetAdjacentBuildings(buildingPlace.GridPosition, this);
        if (neighbors.Count > 0) Debug.Log(neighbors[0]);

        var hasNeighbors = HasNeighbors(buildingPlace.GridPosition);

        if (!isEmptyGrid && !hasNeighbors)
            return false;

        return true;
    }

    public bool HasNeighbors(Vector2Int gridCoordinates)
    {
        if (GridElementsHelper.GetAdjacentBuildings(gridCoordinates, this).Count == 0)
            return false;

        return true;
    }

}

