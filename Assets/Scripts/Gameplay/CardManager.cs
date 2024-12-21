using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private int cardsInHand = 3;
    [SerializeField] private int deckSize = 40;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform cardParent;


    private List<GameObject> cardObjectsInHand = new List<GameObject>();

    void Start()
    {
        var cardType = CardType.SuburbanHouse;
        var cardData = ResourceManager.Instance.GetBuildingCardData(cardType.ToString());
        var card = CardFactory.CreateCard(cardType);
        AddCardGameObject(cardData, card as BuildingCard);
    }

    void Update()
    {
        
    }


    public void AddCardGameObject(BuildingCardData cardData, BuildingCard card)
    {
        cardObjectsInHand.Add(CreateCardGameObject(cardData, card));
    }

    public bool RemoveCardGameObject(BuildingCard card)
    {
        var foundCardObject = cardObjectsInHand.FirstOrDefault(e => e.GetComponent<CardDisplay>().BuildingCard == card);
        if (foundCardObject == null)
            return false;
    
        cardObjectsInHand.Remove(foundCardObject);
        return true;
    }

    private GameObject CreateCardGameObject(BuildingCardData cardData, BuildingCard card)
    {
        GameObject cardObject = Instantiate(cardPrefab, cardParent);

        CardDisplay cardDisplay = cardObject.GetComponent<CardDisplay>();

        cardDisplay.SetCardData(cardData, card);

        return cardObject;
    }

}
