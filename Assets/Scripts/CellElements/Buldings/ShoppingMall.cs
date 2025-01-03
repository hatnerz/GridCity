using UnityEngine.UIElements;

public class ShoppingMall : Building
{
    public ShoppingMall()
        : base("Shopping Mall")
    {

    }

    public override int BaseScore => 6;

    public override BuildingCategory BuildingCategory => BuildingCategory.Commercial;

    public override BuildingType BuildingType => BuildingType.ShoppingMall;

    public override int CalculateTotalBuildingScore(IGridState gridState)
    {
        int totalScore = base.CalculateTotalBuildingScore(gridState);

        var neighbors = gridState.GetNeighbors(this);

        foreach (var neighbor in neighbors)
        {
            if (neighbor is Ground)
            {
                totalScore -= 1;
            }
        }

        return totalScore;
    }
}
