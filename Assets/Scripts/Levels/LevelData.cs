using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Level Data", menuName = "Grid City/Level Data")]
public class LevelData : ScriptableObject
{
    public int levelNumber;
    public List<DeckComposition> deckComposition = new List<DeckComposition>();

    public int targetScore;
}