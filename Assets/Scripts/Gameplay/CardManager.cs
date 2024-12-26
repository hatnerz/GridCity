using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private int maxCardsInHand = 3;
    [SerializeField] private LevelData levelData;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private RectTransform cardParent;
    [SerializeField] private CardSelectionManager selectionManager;

    private List<Card> deckCards = new List<Card>();
    private List<GameObject> cardObjectsInHand = new List<GameObject>();

    public IReadOnlyCollection<GameObject> CardObjectsInHand => cardObjectsInHand.AsReadOnly();
    public int InitialDeckSize { get; private set; }
    public int RemainsCardsInDeck { get => deckCards.Count; }

    public event CardEventHandler OnCardPlayed;
    public event CardEventHandler OnCardTakenFromDeck;

    public delegate void CardEventHandler(GameObject playedCard);

    void Start()
    {
        if(levelData == null)
        {
            throw new MissingReferenceException("LevelData is not set in CardManager");
        }

        InitializeDeck(levelData.DeckComposition);

        for (int i = 0; i < maxCardsInHand; i++)
        {
            TakeCardFromDeck();
        }
    }

    public GameObject AddCardGameObject(BuildingCardData cardData, BuildingCard card)
    {
        var createdCardGameObject = CreateCardGameObject(cardData, card);
        cardObjectsInHand.Add(createdCardGameObject);
        return createdCardGameObject;
    }

    public void InitializeDeck(List<DeckComposition> deck)
    {
        var random = new System.Random();

        InitialDeckSize = deck.Sum(e => e.Count);
        deckCards = deck
            .SelectMany(e => Enumerable.Repeat(e.CardType, e.Count)
                .Select(cardType => CardFactory.CreateCard(cardType)))
            .OrderBy(e => random.Next())
            .ToList();
    }

    public void TakeCardFromDeck()
    {
        if(deckCards.Count == 0)
            return;

        var card = deckCards[0];
        deckCards.RemoveAt(0);

        var takenCard = AddCardGameObject(ResourceManager.Instance.GetBuildingCardData(card.Type.ToString()), card as BuildingCard);
        Debug.Log(takenCard);
        OnCardTakenFromDeck?.Invoke(takenCard);
    }

    public Building TryPlayActiveCard()
    {
        var selectedCard = selectionManager.SelectedCard;
        if (selectedCard == null)
            return null;

        var selectedCardObject = selectedCard.gameObject;

        cardObjectsInHand.Remove(selectedCardObject);

        OnCardPlayed?.Invoke(selectedCardObject);

        if (RemainsCardsInDeck > 0)
            TakeCardFromDeck();

        return selectedCard.BuildingCard.Building;
    }

    private GameObject CreateCardGameObject(BuildingCardData cardData, BuildingCard card)
    {
        GameObject cardObject = Instantiate(cardPrefab, cardParent);

        cardObject.name = card.Building.Name + " card";

        CardDisplay cardDisplay = cardObject.GetComponent<CardDisplay>();

        cardDisplay.SetCardData(cardData, card);

        cardDisplay.SelectionManager = selectionManager;

        return cardObject;
    }
}