using Assets.Scripts.Helpers;
using System.Linq;

public class PowerPlant : Building
{
    public PowerPlant()
        : base("Power Plant")
    {

    }

    public override int BaseScore => 5;

    public override BuildingCategory BuildingCategory => BuildingCategory.Industrial;

    public override BuildingType BuildingType => BuildingType.PowerPlant;

    public override int CalculateTotalBuildingScore(IGridState gridState)
    {
        if (GridPosition == null)
            return BaseScore;

        var adjacentBuildings = GridElementsHelper.GetAdjacentBuildings(GridPosition.Value, gridState);

        var industrialBuildingsCount = adjacentBuildings
            .Where(building => building.BuildingCategory == BuildingCategory.Industrial)
            .Count();

        var bonusPoints = industrialBuildingsCount * 2;

        var noAdjacentPenalty = industrialBuildingsCount == 0 ? -1 : 0;

        return BaseScore + bonusPoints + noAdjacentPenalty;
    }
}
