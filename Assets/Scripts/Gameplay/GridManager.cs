using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private GameObject cellPrefab;

    private Cell[,] grid;

    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        if (grid != null)
        {
            foreach (Cell cell in grid)
            {
                if (cell != null && cell.cellObject != null)
                {
                    Destroy(cell.cellObject);
                }
            }
        }

        grid = new Cell[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 worldPos = GetWorldPosition(x, y);
                GameObject cellObject = Instantiate(cellPrefab, worldPos, Quaternion.identity);
                cellObject.transform.SetParent(transform);
                grid[x, y] = new Cell(cellObject);
            }
        }
    }

    public Vector2 GetWorldPosition(int x, int y)
    {
        return new Vector2(x, y) * cellSize;
    }

    public void GetXY(Vector2 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        y = Mathf.FloorToInt(worldPosition.y / cellSize);
    }
}

public class Cell
{
    public GameObject cellObject;

    public Cell(GameObject cellObject)
    {
        this.cellObject = cellObject;
    }
}