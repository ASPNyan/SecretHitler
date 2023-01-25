using SecretHitlerBackend.Policies;

namespace SecretHitlerGameHandler.Game.GameHandling;

public static class DrawPolicies
{
    public static Policy[] Draw(this List<Policy> Policies)
    {
        Policy[] Output = Policies.Inspect();
        
        Policies.RemoveRange(0, 3);

        return Output;
    }

    public static Policy[] Inspect(this List<Policy> Policies)
    {
        return new[] {
            Policies[0],
            Policies[1],
            Policies[2]
        };
    }
}