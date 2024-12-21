using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public BuildingCardData BuildingCardData { get; private set; }
    public BuildingCard BuildingCard { get; private set; }

    public TMP_Text CardName;
    public TMP_Text CardScore;
    public Image CardImage;
    public TMP_Text CardDescription;

    public void Start()
    {
        UpdateDisplay();
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
}
