using UnityEngine;

public abstract class CellElement
{
    protected CellElement(string name)
    {
        Name = name;
    }

    public string Name { get; set; }

    public Vector2Int? GridPosition { get; set; }

    public bool IsPlaced() => GridPosition != null;
}