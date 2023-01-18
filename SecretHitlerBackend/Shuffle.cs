using SecretHitlerBackend.Policies;

namespace SecretHitlerBackend;

public static class ShufflePolicies
{
    private static readonly Random Random = new();
    
    public static List<T> Shuffle<T>(this List<T> Items)
    {
        do
        {
            int Length = Items.Count;
            while (Length > 1)
            {
                Length--;
                int Rng = Random.Next(Length + 1);
                (Items[Rng], Items[Length]) = (Items[Length], Items[Rng]);
            }
        } while (Wait(1500));

        return Items;
    }

    private static bool Wait(int MillisecondDelay)
    {
        Task.Delay(MillisecondDelay);
        return false;
    }
}