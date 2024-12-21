using UnityEngine;

public class GridManager : MonoBehaviour, IGridState
{
    [SerializeField] private int sizeX = 10;
    [SerializeField] private int sizeY = 10;

    public int SizeX { get { return sizeX; } }
    public int SizeY { get { return sizeY; } }

    public CellElement[,] GridElements { get; private set; }

    void Start()
    {
    }
}