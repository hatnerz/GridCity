using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCard : Card
{
    public BuildingCard(CardCategory category, CardType type, Building building) 
        : base(category, type)
    {
        Building = building;
    }

    public Building Building { get; private set; }
}
