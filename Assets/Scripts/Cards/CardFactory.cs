using System.Collections.Generic;
using System;

public class CardFactory
{
    private static Dictionary<CardType, Func<Card>> buildingCreators =
        new Dictionary<CardType, Func<Card>>
    {
        { CardType.SuburbanHouse, () => new BuildingCard(
            CardCategory.Building, 
            CardType.SuburbanHouse, 
            BuildingFactory.CreateBuilding(BuildingType.SuburbanHouse)) },

        { CardType.Park, () => new BuildingCard(
            CardCategory.Building,
            CardType.Park,
            BuildingFactory.CreateBuilding(BuildingType.Park)) },

        { CardType.ShoppingMall, () => new BuildingCard(
            CardCategory.Building,
            CardType.ShoppingMall,
            BuildingFactory.CreateBuilding(BuildingType.ShoppingMall)) },

    };

    public static Card CreateCard(CardType type)
    {
        if (buildingCreators.TryGetValue(type, out var creator))
        {
            return creator();
        }

        throw new ArgumentException($"Unknown card  type: {type}");
    }
}
