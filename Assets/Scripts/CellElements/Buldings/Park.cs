public class Park : Building
{
    public Park()
        : base("Park")
    {

    }

    public override int BaseScore => 3;

    public override BuildingCategory BuildingCategory => BuildingCategory.Facilities;

    public override BuildingType BuildingType => BuildingType.Park;

    public override int CalculateTotalBuildingScore(IGridState gridState)
    {
        int totalBuildingScore = base.CalculateTotalBuildingScore(gridState);

        var neighbors = gridState.GetNeighbors(this);

        foreach (var neighbor in neighbors)
        {
            if (neighbor is Building neighborBuilding &&
                neighborBuilding.BuildingCategory == BuildingCategory.Residential)
            {
                totalBuildingScore += 1;
            }
        }

        return totalBuildingScore;
    }
}
