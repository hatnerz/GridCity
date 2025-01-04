using Assets.Scripts.Helpers;
using System.Linq;

public class Factory : Building
{
    public Factory()
        : base("Factory")
    {

    }

    public override int BaseScore => 3;

    public override BuildingCategory BuildingCategory => BuildingCategory.Industrial;

    public override BuildingType BuildingType => BuildingType.Factory;

    public override int CalculateTotalBuildingScore(IGridState gridState)
    {
        if (GridPosition == null)
            return BaseScore;

        var adjacentBuildings = GridElementsHelper.GetAdjacentBuildings(GridPosition.Value, gridState);

        var bonusPoints = adjacentBuildings
            .Where(building => building.BuildingCategory == BuildingCategory.Industrial)
            .Count();

        return BaseScore + bonusPoints;
    }
}
