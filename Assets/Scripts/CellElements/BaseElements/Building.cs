using UnityEngine;

public abstract class Building : CellElement
{
    protected Building(string name)
        : base(name)
    {
    }

    public Sprite Sprite { get; private set; }

    public abstract int BaseScore { get; }

    public virtual int CalculateTotalBuildingScore(IGridState gridState) => BaseScore;
}
