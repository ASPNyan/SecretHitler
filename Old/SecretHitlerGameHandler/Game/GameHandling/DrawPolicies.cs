using SecretHitlerBackend.Policies;

namespace SecretHitlerGameHandler.Game.GameHandling;

public static class DrawPolicies
{
    public static Policy[] Draw(this List<Policy> policies)
    {
        Policy[] output = policies.Inspect();
        
        policies.RemoveRange(0, 3);

        return output;
    }

    public static Policy[] Inspect(this List<Policy> policies)
    {
        return new[] {
            policies[0],
            policies[1],
            policies[2]
        };
    }
}