using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour, IPointerClickHandler
{
    private bool isHighlighted;
    private Outline outline;

    public TMP_Text CardName;
    public TMP_Text CardScore;
    public Image CardImage;
    public TMP_Text CardDescription;
    public GameObject BackgroundOutlineObject;

    public BuildingCardData BuildingCardData { get; private set; }
    public BuildingCard BuildingCard { get; private set; }
    public CardSelectionManager SelectionManager { get; set; }


    public void Start()
    {
        UpdateDisplay();
        InitializeOutline();
    }

    public void SetCardData(BuildingCardData data, BuildingCard card)
    {
        BuildingCardData = data;
        BuildingCard = card;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (BuildingCardData != null)
        {
            CardImage.sprite = BuildingCardData.CardSprite;
            CardDescription.text = string.Join(Environment.NewLine, BuildingCardData.EffectsDescriptions);
        }

        if (BuildingCard != null)
        {
            CardName.text = BuildingCard.Building.Name;
            CardScore.text = BuildingCard.Building.BaseScore.ToString();
        }
    }

    public void SetHighlight(bool isHighlighted)
    {
        this.isHighlighted = isHighlighted;
        outline.enabled = isHighlighted;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SelectionManager.SelectCard(this);
    }

    private void InitializeOutline()
    {
        outline = BackgroundOutlineObject.AddComponent<Outline>();
        outline.effectColor = new Color(1f, 0.5f, 0f, 1f);
        outline.effectDistance = new Vector2(3f, -3f);
        outline.useGraphicAlpha = false;
        outline.enabled = false;
    }
}
