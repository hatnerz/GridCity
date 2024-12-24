public class Park : Building
{
    public Park()
        : base("Park")
    {

    }

    public override int BaseScore => 3;

    public override BuildingCategory BuildingCategory => BuildingCategory.Facilities;

    public override int CalculateTotalBuildingScore(IGridState gridState)
    {
        return base.CalculateTotalBuildingScore(gridState) + 1;
    }
}
