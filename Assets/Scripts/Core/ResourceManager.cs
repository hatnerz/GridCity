using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    [SerializeField] private List<BuildingCardData> buildingCardsData;
    [SerializeField] private List<BuildingData> buildingsData;

    private Dictionary<string, BuildingCardData> cardDataDictionary;
    private Dictionary<BuildingType, BuildingData> buildingDataDictionary;

    public IReadOnlyDictionary<BuildingType, BuildingData> BuildingDataDictionary { get { return buildingDataDictionary; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeCardDictionary();
            InitializeBuildingsDictionary();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeCardDictionary()
    {
        cardDataDictionary = new Dictionary<string, BuildingCardData>();

        foreach (var card in buildingCardsData)
        {
            cardDataDictionary[card.name] = card;
        }
    }

    private void InitializeBuildingsDictionary()
    {
        buildingDataDictionary = new Dictionary<BuildingType, BuildingData>();

        foreach (var building in buildingsData)
        {
            buildingDataDictionary[building.BuildingType] = building;
        }
    }

    public BuildingCardData GetBuildingCardData(string cardName)
    {
        if (cardDataDictionary.TryGetValue(cardName, out BuildingCardData cardData))
        {
            return cardData;
        }

        Debug.LogWarning($"BuildingCardData not found for card name: {cardName}");
        return null;
    }
}