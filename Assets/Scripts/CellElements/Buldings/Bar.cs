using Assets.Scripts.Helpers;
using System.Linq;
using UnityEngine;

public class Bar : Building
{
    public Bar()
        : base("Bar")
    {

    }

    public override int BaseScore => 3;

    public override BuildingCategory BuildingCategory => BuildingCategory.Commercial;

    public override BuildingType BuildingType => BuildingType.Bar;

    public override int CalculateTotalBuildingScore(IGridState gridState)
    {
        if (GridPosition == null)
            return BaseScore;

        var adjacentBuildings = GridElementsHelper.GetAdjacentBuildings(GridPosition.Value, gridState);

        int residentialBonus = Mathf.Min(
            adjacentBuildings.Where(e => e.BuildingCategory == BuildingCategory.Residential).Count(),
            2
        ) * 2;

        int industrialPenalty = adjacentBuildings
            .Where(e => e.BuildingCategory == BuildingCategory.Industrial)
            .Count();

        var finalScore = BaseScore + residentialBonus - industrialPenalty;

        return finalScore;
    }
}
