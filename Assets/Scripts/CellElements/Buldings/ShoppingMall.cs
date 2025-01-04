using Assets.Scripts.Helpers;
using UnityEngine;

public class ShoppingMall : Building
{
    public ShoppingMall()
        : base("Shopping Mall")
    {
    }

    public override int BaseScore => 8;

    public override BuildingCategory BuildingCategory => BuildingCategory.Commercial;

    public override BuildingType BuildingType => BuildingType.ShoppingMall;

    public override int CalculateTotalBuildingScore(IGridState gridState)
    {
        if (GridPosition == null)
            return BaseScore;

        var adjacentBuildings = GridElementsHelper.GetAdjacentBuildings(GridPosition.Value, gridState);
        return BaseScore - (4 - adjacentBuildings.Count);
    }
}
