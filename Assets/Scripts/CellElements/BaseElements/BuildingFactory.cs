using System;
using System.Collections.Generic;

public class BuildingFactory
{
    private static Dictionary<BuildingType, Func<Building>> buildingCreators =
        new Dictionary<BuildingType, Func<Building>>
    {
        { BuildingType.SuburbanHouse, () => new SuburbanHouse() },
        { BuildingType.Park, () => new Park() },
        { BuildingType.ShoppingMall, () => new ShoppingMall() },
        { BuildingType.Factory, () => new Factory() }
    };

    public static Building CreateBuilding(BuildingType type)
    {
        if (buildingCreators.TryGetValue(type, out var creator))
        {
            return creator();
        }
        throw new ArgumentException($"Unknown building type: {type}");
    }
}