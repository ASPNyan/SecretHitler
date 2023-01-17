namespace SecretHitlerBackend.Policies;

public class Policies
{
    public List<Policy> Deck = new();

    public Policies(DeckSize Size)
    {
        for (int I = 1; I <= 17 * (int)Size; I++)
        {
            Deck.Add(I <= 6 * (int)Size ? Policy.Liberal : Policy.Fascist);
        }
        Deck.Shuffle();
    }
}