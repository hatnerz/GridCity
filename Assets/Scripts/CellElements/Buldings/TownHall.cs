public class TownHall : Building
{
    public TownHall()
        : base("TownHall")
    {

    }

    public override int BaseScore => 5;

    public override BuildingCategory BuildingCategory => BuildingCategory.Special;

    public override BuildingType BuildingType => BuildingType.TownHall;

    public override int CalculateTotalBuildingScore(IGridState gridState)
    {
        return base.CalculateTotalBuildingScore(gridState) + 1;
    }
}
