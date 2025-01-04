public class PowerPlant : Building
{
    public PowerPlant()
        : base("Power Plant")
    {

    }

    public override int BaseScore => 3;

    public override BuildingCategory BuildingCategory => BuildingCategory.Industrial;

    public override BuildingType BuildingType => BuildingType.PowerPlant;

    public override int CalculateTotalBuildingScore(IGridState gridState)
    {
        return base.CalculateTotalBuildingScore(gridState) + 1;
    }
}
