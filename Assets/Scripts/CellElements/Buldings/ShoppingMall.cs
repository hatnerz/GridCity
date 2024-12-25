public class ShoppingMall : Building
{
    public ShoppingMall()
        : base("Shopping Mall")
    {

    }

    public override int BaseScore => 6;

    public override BuildingCategory BuildingCategory => BuildingCategory.Commercial;

    public override int CalculateTotalBuildingScore(IGridState gridState)
    {
        return base.CalculateTotalBuildingScore(gridState) + 1;
    }
}
