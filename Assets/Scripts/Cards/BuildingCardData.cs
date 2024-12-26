using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building Card Data", menuName = "Grid City/Building Card Data")]
public class BuildingCardData : ScriptableObject
{
    public Sprite CardSprite;
    public List<string> EffectsDescriptions;
}
