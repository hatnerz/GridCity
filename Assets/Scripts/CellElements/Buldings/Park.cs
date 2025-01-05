using Assets.Scripts.Helpers;
using System.Linq;
using UnityEngine;

public class Park : Building
{
    public Park()
        : base("Park")
    {
    }

    public override int BaseScore => 3;

    public override BuildingCategory BuildingCategory => BuildingCategory.Facilities;

    public override BuildingType BuildingType => BuildingType.Park;

    public override int CalculateTotalBuildingScore(IGridState gridState)
    {
        if (GridPosition == null)
            return BaseScore;

        var adjacentBuildings = GridElementsHelper.GetAdjacentBuildings(GridPosition.Value, gridState);

        int additionalPoints = adjacentBuildings
            .Where(e => e.BuildingCategory == BuildingCategory.Commercial || e.BuildingCategory == BuildingCategory.Residential)
            .Count();

        additionalPoints = Mathf.Min(additionalPoints, 2);

        int penaltyPoints = adjacentBuildings
            .Where(e => e.BuildingCategory == BuildingCategory.Industrial)
            .Count();

        return BaseScore + additionalPoints - penaltyPoints;
    }

}
