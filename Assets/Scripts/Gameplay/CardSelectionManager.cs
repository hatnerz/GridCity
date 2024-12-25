using UnityEngine;

public class CardSelectionManager : MonoBehaviour
{
    private CardDisplay selectedCard;

    public void SelectCard(CardDisplay card)
    {
        if (selectedCard != null)
        {
            selectedCard.SetHighlight(false);
        }

        if (selectedCard != card)
        {
            selectedCard = card;
            selectedCard.SetHighlight(true);
        }
        else
        {
            selectedCard = null;
        }
    }
}
