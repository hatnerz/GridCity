public class ApartmentBuilding : Building
{
    public ApartmentBuilding()
        : base("Apartment Building")
    {

    }

    public override int BaseScore => 3;

    public override BuildingCategory BuildingCategory => BuildingCategory.Residential;

    public override BuildingType BuildingType => BuildingType.ApartmentBuilding;

    public override int CalculateTotalBuildingScore(IGridState gridState)
    {
        return base.CalculateTotalBuildingScore(gridState) + 1;
    }
}
