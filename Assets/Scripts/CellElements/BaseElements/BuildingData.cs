using UnityEngine;

[CreateAssetMenu(fileName = "New Building Type", menuName = "Grid City/Building Type")]
public class BuildingTypeData : ScriptableObject
{
    public string buildingName;
    public Sprite buildingSprite;
    public int baseScore;
}