using UnityEngine;

public class SuburbanHouse : Building
{
    public SuburbanHouse() 
        : base("Suburban House")
    {
        
    }

    public override int BaseScore => 8;

    public override BuildingCategory BuildingCategory => BuildingCategory.Residential;

    public override int CalculateTotalBuildingScore(IGridState gridState)
    {
        return base.CalculateTotalBuildingScore(gridState) + 1;
    }
}
