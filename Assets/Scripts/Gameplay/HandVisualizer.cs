using UnityEngine;
using UnityEngine.UIElements;

public class HandVisualizer : MonoBehaviour
{
    [SerializeField] private CardManager cardManager;
    [SerializeField] private int cardSpacing = 100;

    public void Start()
    {
        if(cardManager != null)
        {
            SetCardsPositions();
            cardManager.OnCardPlayed += (_) => SetCardsPositions();
        }
    }

    public void SetCardsPositions()
    {
        var cardsToDisplay = cardManager.CardObjectsInHand;
        if (cardsToDisplay.Count == 0)
            return;

        int positionX = 0;

        if (cardsToDisplay.Count % 2 == 0)
            positionX = -(cardsToDisplay.Count / 2 * cardSpacing - cardSpacing / 2);
        else
            positionX = -(cardsToDisplay.Count / 2 * cardSpacing);

        foreach (var card in cardsToDisplay)
        {
            Debug.Log(positionX);
            card.GetComponent<RectTransform>().anchoredPosition = new Vector2(positionX, 0);
            positionX += cardSpacing;
        }
    }
}
