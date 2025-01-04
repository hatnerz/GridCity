using Assets.Scripts.Helpers;
using System.Linq;

public class University : Building
{
    public University()
        : base("University")
    {

    }

    public override int BaseScore => 5;

    public override BuildingCategory BuildingCategory => BuildingCategory.Facilities;

    public override BuildingType BuildingType => BuildingType.University;

    public override int CalculateTotalBuildingScore(IGridState gridState)
    {
        if (GridPosition == null)
            return BaseScore;

        var adjacentBuildings = GridElementsHelper.GetAdjacentBuildings(GridPosition.Value, gridState);
        var rowAndColumnBuildings = GridElementsHelper.GetBuildingsInRowAndColumn(GridPosition.Value, gridState);

        int universityPenalty = rowAndColumnBuildings
            .Where(building => building.BuildingType == BuildingType.University && building != this)
            .Count() * -2;

        int facilitiesBonus = adjacentBuildings
            .Where(building => building.BuildingCategory == BuildingCategory.Facilities)
            .Count();

        return BaseScore + universityPenalty + facilitiesBonus;
    }
}
