namespace SecretHitlerBackend;

public static class ShufflePolicies
{
    private static readonly Random Random = new();
    
    public static List<T> Shuffle<T>(this List<T> items)
    {
        do
        {
            int length = items.Count;
            while (length > 1)
            {
                length--;
                int rng = Random.Next(length + 1);
                (items[rng], items[length]) = (items[length], items[rng]);
            }
        } while (Wait(1500));

        return items;
    }

    private static bool Wait(int millisecondDelay)
    {
        Task.Delay(millisecondDelay);
        return false;
    }
}