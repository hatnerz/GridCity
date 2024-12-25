using UnityEngine;

public interface IGridState
{
    CellElement[,] GridElements { get; }

    int SizeX { get; }
    int SizeY { get; }
}

