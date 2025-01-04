using Assets.Scripts.Helpers;
using System.Linq;

public class Bar : Building
{
    public Bar()
        : base("Bar")
    {

    }

    public override int BaseScore => 4;

    public override BuildingCategory BuildingCategory => BuildingCategory.Commercial;

    public override BuildingType BuildingType => BuildingType.Bar;

    public override int CalculateTotalBuildingScore(IGridState gridState)
    {
        if (GridPosition == null)
            return BaseScore;

        var adjacentBuildings = GridElementsHelper.GetAdjacentBuildings(GridPosition.Value, gridState);

        var finalScore = BaseScore
            + adjacentBuildings.Where(e => e.BuildingCategory == BuildingCategory.Residential).Count() * 2;

        return finalScore;
    }
}
