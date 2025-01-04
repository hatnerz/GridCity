using Assets.Scripts.Helpers;
using System.Linq;

public class ApartmentBuilding : Building
{
    public ApartmentBuilding()
        : base("Apartment Building")
    {

    }

    public override int BaseScore => 5;

    public override BuildingCategory BuildingCategory => BuildingCategory.Residential;

    public override BuildingType BuildingType => BuildingType.ApartmentBuilding;

    public override int CalculateTotalBuildingScore(IGridState gridState)
    {
        if (GridPosition == null)
            return BaseScore;

        var adjacentBuildings = GridElementsHelper.GetAdjacentBuildings(GridPosition.Value, gridState);

        var bonusPoints = adjacentBuildings
            .Where(building => building.BuildingCategory == BuildingCategory.Residential)
            .Count();

        var penaltyPoints = adjacentBuildings
            .Where(building => building.BuildingCategory == BuildingCategory.Industrial)
            .Count() * -2;

        return BaseScore + bonusPoints + penaltyPoints;
    }
}
