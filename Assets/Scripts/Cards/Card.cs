public class Card
{
    public Card(CardCategory category, CardType type)
    {
        Category = category;
        Type = type;
    }

    public CardCategory Category { get; private set; }
    public CardType Type { get; private set; }
}