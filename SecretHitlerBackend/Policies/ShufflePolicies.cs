namespace SecretHitlerBackend.Policies;

public static class ShufflePolicies
{
    private static Random Random = new();
    
    public static void Shuffle(this List<Policy> Policies)
    {
        int Length = Policies.Count;
        while (Length > 1)
        {
            Length--;
            int Rng = Random.Next(Length + 1);
            (Policies[Rng], Policies[Length]) = (Policies[Length], Policies[Rng]);
        }
    }
}