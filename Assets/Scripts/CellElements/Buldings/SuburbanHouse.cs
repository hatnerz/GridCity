using Assets.Scripts.Helpers;
using System.Linq;
using UnityEngine;

public class SuburbanHouse : Building
{
    public SuburbanHouse() 
        : base("Suburban House")
    {
        
    }

    public override int BaseScore => 4;

    public override BuildingCategory BuildingCategory => BuildingCategory.Residential;

    public override BuildingType BuildingType => BuildingType.SuburbanHouse;

    public override int CalculateTotalBuildingScore(IGridState gridState)
    {
        if (GridPosition == null)
            return BaseScore;

        var adjacentBuildings = GridElementsHelper.GetAdjacentBuildings(GridPosition.Value, gridState);

        var finalScore = BaseScore
            + adjacentBuildings.Where(e => e.BuildingCategory == BuildingCategory.Facilities).Count()
            - adjacentBuildings.Where(e => e.BuildingCategory == BuildingCategory.Industrial).Count();

        return finalScore;
    }
}
