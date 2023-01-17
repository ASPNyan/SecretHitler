namespace SecretHitlerBackend.Policies;

public static class ShufflePolicies
{
    private static readonly Random Random = new();
    
    public static void Shuffle(this List<Policy> Policies)
    {
        do
        {
            int Length = Policies.Count;
            while (Length > 1)
            {
                Length--;
                int Rng = Random.Next(Length + 1);
                (Policies[Rng], Policies[Length]) = (Policies[Length], Policies[Rng]);
            }
        } while (Wait(1500));
    }

    private static bool Wait(int MillisecondDelay)
    {
        Task.Delay(MillisecondDelay);
        return false;
    }
}