public class NeighborhoodShop : Building
{
    public NeighborhoodShop()
        : base("Neighborhood Shop")
    {

    }

    public override int BaseScore => 6;

    public override BuildingCategory BuildingCategory => BuildingCategory.Commercial;

    public override BuildingType BuildingType => BuildingType.NeighborhoodShop;

    public override int CalculateTotalBuildingScore(IGridState gridState)
    {
        return base.CalculateTotalBuildingScore(gridState) + 1;
    }
}
