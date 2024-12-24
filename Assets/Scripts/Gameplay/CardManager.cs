using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private int cardsInHand = 3;
    [SerializeField] private int deckSize = 40;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private RectTransform cardParent;

    private List<Card> deckCards = new List<Card>();
    private List<GameObject> cardObjectsInHand = new List<GameObject>();

    public IReadOnlyCollection<GameObject> CardObjectsInHand => cardObjectsInHand.AsReadOnly();

    public event CardPlayedEventHandler OnCardPlayed;


    void Start()
    {
        var cardType = CardType.SuburbanHouse;
        var cardData = ResourceManager.Instance.GetBuildingCardData(cardType.ToString());
        var card = CardFactory.CreateCard(cardType);
        AddCardGameObject(cardData, card as BuildingCard);

        var cardType2 = CardType.Park;
        var cardData2 = ResourceManager.Instance.GetBuildingCardData(cardType2.ToString());
        var card2 = CardFactory.CreateCard(cardType2);
        AddCardGameObject(cardData2, card2 as BuildingCard);

        var cardType3 = CardType.ShoppingMall;
        var cardData3 = ResourceManager.Instance.GetBuildingCardData(cardType3.ToString());
        var card3 = CardFactory.CreateCard(cardType3);
        var playedCard = AddCardGameObject(cardData3, card3 as BuildingCard);

        OnCardPlayed(playedCard);
    }

    void Update()
    {
        
    }


    public GameObject AddCardGameObject(BuildingCardData cardData, BuildingCard card)
    {
        var createdCardGameObject = CreateCardGameObject(cardData, card);
        cardObjectsInHand.Add(createdCardGameObject);
        return createdCardGameObject;
    }

    public bool RemoveCardGameObject(BuildingCard card)
    {
        var foundCardObject = cardObjectsInHand.FirstOrDefault(e => e.GetComponent<CardDisplay>().BuildingCard == card);
        if (foundCardObject == null)
            return false;
    
        cardObjectsInHand.Remove(foundCardObject);
        return true;
    }

    public void Initialize(List<DeckComposition> deck)
    {
        deckSize = deck.Sum(e => e.Count);
        deckCards = deck
            .SelectMany(e => Enumerable.Repeat(e.CardType, e.Count)
                .Select(cardType => CardFactory.CreateCard(cardType)))
            .ToList();
    }

    private GameObject CreateCardGameObject(BuildingCardData cardData, BuildingCard card)
    {
        GameObject cardObject = Instantiate(cardPrefab, cardParent);

        CardDisplay cardDisplay = cardObject.GetComponent<CardDisplay>();

        cardDisplay.SetCardData(cardData, card);

        return cardObject;
    }

}

public delegate void CardPlayedEventHandler(GameObject playedCard);