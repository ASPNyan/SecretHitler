namespace SecretHitlerBackend.Policies;

public class Policies
{
    public List<Policy> Deck = new();

    public Policies()
    {
        for (int I = 1; I <= 51; I++)
        {
            Deck.Add(I <= 18 ? Policy.Liberal : Policy.Fascist);
        }
        Deck.Shuffle();
    }
}