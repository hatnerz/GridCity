using Assets.Scripts.Helpers;
using System.Linq;

public class Park : Building
{
    public Park()
        : base("Park")
    {
    }

    public override int BaseScore => 2;

    public override BuildingCategory BuildingCategory => BuildingCategory.Facilities;

    public override BuildingType BuildingType => BuildingType.Park;

    public override int CalculateTotalBuildingScore(IGridState gridState)
    {
        if (GridPosition == null)
            return BaseScore;

        var adjacentBuildings = GridElementsHelper.GetAdjacentBuildings(GridPosition.Value, gridState);

        var finalScore = BaseScore
            + adjacentBuildings.Where(e => e.BuildingCategory == BuildingCategory.Commercial).Count();
  

        return finalScore;
    }
}
