using Assets.Scripts.Helpers;
using System.Linq;
using UnityEngine;

public class Hospital : Building
{
    public Hospital()
        : base("Hospital")
    {

    }

    public override int BaseScore => 4;

    public override BuildingCategory BuildingCategory => BuildingCategory.Facilities;

    public override BuildingType BuildingType => BuildingType.Hospital;

    public override int CalculateTotalBuildingScore(IGridState gridState)
    {
        if (GridPosition == null)
            return BaseScore;

        var buildingsInRadius = GridElementsHelper.GetBuildingsInRadius(GridPosition.Value, 1, gridState);

        var bonusPoints = Mathf.Min(
            buildingsInRadius
                .Where(building => building.BuildingCategory == BuildingCategory.Residential ||
                                   building.BuildingCategory == BuildingCategory.Facilities)
                .Count(),
            5
        );

        var penaltyPoints = buildingsInRadius
            .Where(building => building.BuildingCategory == BuildingCategory.Industrial)
            .Count() * 2;

        return BaseScore + bonusPoints - penaltyPoints;
    }


}
