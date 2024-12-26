using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class HandVisualizer : MonoBehaviour
{
    [SerializeField] private CardManager cardManager;
    [SerializeField] private int cardSpacing = 100;

    private float throwHeight = 2f;
    private float throwDuration = 0.5f;
    private AnimationCurve throwCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    private AnimationCurve fadeCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

    private bool isAnimating = false;

    public void Start()
    {
        if(cardManager != null)
        {
            SetCardsPositions();
            cardManager.OnCardPlayed += (card) => HandleCardRemoving(card);
            cardManager.OnCardTakenFromDeck += (card) => HandleCardTake(card);
        }
    }

    private void HandleCardRemoving(GameObject cardObject)
    {
        StartCoroutine(ThrowCardCoroutine(cardObject));
    }

    private void HandleCardTake(GameObject cardObject)
    {
        cardObject.SetActive(false);
        StartCoroutine(WaitAndSetCardsPositions());
    }

    private IEnumerator WaitAndSetCardsPositions()
    {
        while (isAnimating)
        {
            yield return null;
        }
        SetCardsPositions();
    }

    private void SetCardsPositions()
    {
        var cardsToDisplay = cardManager.CardObjectsInHand;
        if (cardsToDisplay.Count == 0)
            return;

        Debug.Log("Cards in hand : " + cardsToDisplay.Count);

        int positionX = 0;

        if (cardsToDisplay.Count % 2 == 0)
            positionX = -(cardsToDisplay.Count / 2 * cardSpacing - cardSpacing / 2);
        else
            positionX = -(cardsToDisplay.Count / 2 * cardSpacing);

        foreach (var card in cardsToDisplay)
        {
            card.SetActive(true);
            card.GetComponent<RectTransform>().anchoredPosition = new Vector2(positionX, 0);
            positionX += cardSpacing;
        }
    }

    private IEnumerator ThrowCardCoroutine(GameObject cardObject)
    {
        isAnimating = true;
        var canvasGroup = cardObject.GetComponent<CanvasGroup>();
        var rectTransform = cardObject.GetComponent<RectTransform>();
        Vector2 startPosition = rectTransform.anchoredPosition;
        Vector2 endPosition = startPosition + Vector2.up * throwHeight;

        float elapsedTime = 0f;
        while (elapsedTime < throwDuration)
        {
            float t = elapsedTime / throwDuration;
            float yOffset = throwCurve.Evaluate(t) * throwHeight;
            rectTransform.anchoredPosition = startPosition + Vector2.up * yOffset;
            canvasGroup.alpha = 1f - fadeCurve.Evaluate(t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = endPosition;
        canvasGroup.alpha = 0f;
        Destroy(cardObject);
        SetCardsPositions();
        isAnimating = false;
    }
}
