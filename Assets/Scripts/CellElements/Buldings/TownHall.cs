using Assets.Scripts.Helpers;
using System.Linq;

public class TownHall : Building
{
    public TownHall()
        : base("Town Hall")
    {

    }

    public override int BaseScore => 1;

    public override BuildingCategory BuildingCategory => BuildingCategory.Special;

    public override BuildingType BuildingType => BuildingType.TownHall;

    public override int CalculateTotalBuildingScore(IGridState gridState)
    {
        if (GridPosition == null)
            return BaseScore;

        var adjacentBuildings = GridElementsHelper.GetAdjacentBuildings(GridPosition.Value, gridState);

        var random = new System.Random();
        var selectedBuildings = adjacentBuildings
            .OrderBy(_ => random.Next())
            .Take(2)
            .ToList();

        int bonusPoints = selectedBuildings.Sum(building => building.CalculateTotalBuildingScore(gridState));

        return BaseScore + bonusPoints;
    }
}
