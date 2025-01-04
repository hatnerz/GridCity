using Assets.Scripts.Helpers;
using System.Linq;

public class Office : Building
{
    public Office()
        : base("Office")
    {

    }

    public override int BaseScore => 3;

    public override BuildingCategory BuildingCategory => BuildingCategory.Commercial;

    public override BuildingType BuildingType => BuildingType.Office;

    public override int CalculateTotalBuildingScore(IGridState gridState)
    {
        if (GridPosition == null)
            return BaseScore;

        var adjacentBuildings = GridElementsHelper.GetAdjacentBuildings(GridPosition.Value, gridState);

        var finalScore = BaseScore
            + adjacentBuildings.Where(e => e.BuildingCategory == BuildingCategory.Commercial).Count()
            + adjacentBuildings.Where(e => e.BuildingCategory == BuildingCategory.Facilities).Count();

        return finalScore;
    }

}
