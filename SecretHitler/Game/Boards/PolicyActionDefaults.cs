namespace SecretHitler.Game.Boards;

public static class PolicyActionDefaults
{
    private static readonly PolicyAction[][] FiveOrSix =
    { 
        new [] { PolicyAction.None },
        new [] { PolicyAction.None },
        new [] { PolicyAction.PolicyPeek },
        new [] { PolicyAction.Execute },
        new [] { PolicyAction.Execute, PolicyAction.Veto }
    };

    private static readonly PolicyAction[][] SevenOrEight = 
    {
        new [] { PolicyAction.None },
        new [] { PolicyAction.InvestigateLoyalty },
        new [] { PolicyAction.CallSpecialElection },
        new [] { PolicyAction.Execute },
        new [] { PolicyAction.Execute, PolicyAction.Veto }
    };

    private static readonly PolicyAction[][] NineOrTen =
    {
        new [] { PolicyAction.InvestigateLoyalty },
        new [] { PolicyAction.InvestigateLoyalty },
        new [] { PolicyAction.CallSpecialElection },
        new [] { PolicyAction.Execute },
        new [] { PolicyAction.Execute, PolicyAction.Veto }
    };

    public static PolicyAction[][] GetActions(byte playerCount)
    {
        string errorDefault = $"Expected: 5 <= count <= 10. Received: {playerCount}";
        
        return playerCount switch
        {
            >= 11 => throw new ArgumentOutOfRangeException(nameof(playerCount),
                $"No default player count should exceed 10. {errorDefault}"),
            >= 9 => NineOrTen,
            >= 7 => SevenOrEight,
            >= 5 => FiveOrSix,
            _ => throw new ArgumentOutOfRangeException(nameof(playerCount),
                $"No default player count should be less than 5. {errorDefault}")
        };
    }
}