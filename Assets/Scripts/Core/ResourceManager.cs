using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    [SerializeField] private List<CardData> buildingCardsData;
    [SerializeField] private List<BuildingData> buildingsData;

    private Dictionary<CardType, CardData> cardDataDictionary;
    private Dictionary<BuildingType, BuildingData> buildingDataDictionary;

    public IReadOnlyDictionary<BuildingType, BuildingData> BuildingDataDictionary { get { return buildingDataDictionary; } }
    public IReadOnlyDictionary<CardType, CardData> CardDataDictionary { get { return cardDataDictionary; } }


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
        cardDataDictionary = new Dictionary<CardType, CardData>();

        foreach (var card in buildingCardsData)
        {
            cardDataDictionary[card.CardType] = card;
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
}