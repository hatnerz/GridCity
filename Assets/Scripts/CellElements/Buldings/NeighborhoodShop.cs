using Assets.Scripts.Helpers;
using System.Linq;

public class NeighborhoodShop : Building
{
    public NeighborhoodShop()
        : base("Neighborhood Shop")
    {

    }

    public override int BaseScore => 3;

    public override BuildingCategory BuildingCategory => BuildingCategory.Commercial;

    public override BuildingType BuildingType => BuildingType.NeighborhoodShop;

    public override int CalculateTotalBuildingScore(IGridState gridState)
    {
        if (GridPosition == null)
            return BaseScore;

        var adjacentBuildings = GridElementsHelper.GetAdjacentBuildings(GridPosition.Value, gridState);

        var finalScore = BaseScore
            + adjacentBuildings.Where(e => e.BuildingCategory == BuildingCategory.Residential).Count();

        return finalScore;
    }
}
