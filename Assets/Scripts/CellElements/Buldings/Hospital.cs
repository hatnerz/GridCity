public class Hospital : Building
{
    public Hospital()
        : base("Hospital")
    {

    }

    public override int BaseScore => 2;

    public override BuildingCategory BuildingCategory => BuildingCategory.Facilities;

    public override BuildingType BuildingType => BuildingType.Hospital;

    public override int CalculateTotalBuildingScore(IGridState gridState)
    {
        return base.CalculateTotalBuildingScore(gridState) + 1;
    }
}
