public class Factory : Building
{
    public Factory()
        : base("Factory")
    {

    }

    public override int BaseScore => 5;

    public override BuildingCategory BuildingCategory => BuildingCategory.Industrial;

    public override BuildingType BuildingType => BuildingType.Factory;

    public override int CalculateTotalBuildingScore(IGridState gridState)
    {
        return base.CalculateTotalBuildingScore(gridState) + 1;
    }
}
