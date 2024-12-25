using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    [SerializeField] private List<BuildingCardData> buildingCardsData;
    private Dictionary<string, BuildingCardData> cardDataDict;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeCardDictionary();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeCardDictionary()
    {
        cardDataDict = new Dictionary<string, BuildingCardData>();

        foreach (var card in buildingCardsData)
        {
            cardDataDict[card.name] = card;
        }
    }

    public BuildingCardData GetBuildingCardData(string cardName)
    {
        if (cardDataDict.TryGetValue(cardName, out BuildingCardData cardData))
        {
            return cardData;
        }

        Debug.LogWarning($"BuildingCardData not found for card name: {cardName}");
        return null;
    }
}