using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building Card", menuName = "Grid City/Building Card")]
public class BuildingCardData : ScriptableObject
{
    public Sprite CardSprite;
    public List<string> EffectsDescriptions;
}
