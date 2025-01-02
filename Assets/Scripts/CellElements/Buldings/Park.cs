public class Park : Building
{
    public Park()
        : base("Park")
    {
    }

    public override int BaseScore => 4;

    public override BuildingCategory BuildingCategory => BuildingCategory.Facilities;

    public override BuildingType BuildingType => BuildingType.Park;
}
