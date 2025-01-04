using Assets.Scripts.Helpers;
using System.Linq;

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

        var bonusPoints = buildingsInRadius
            .Where(building => building.BuildingCategory == BuildingCategory.Residential ||
                               building.BuildingCategory == BuildingCategory.Facilities)
            .Count();

        return BaseScore + bonusPoints;
    }
}
