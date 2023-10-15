namespace SecretHitlerBackend.Boards.FascistBoard;

public static class Actions
{
    private static readonly Dictionary<byte, PolicyAction> FiveOrSix = new()
    {
        { 1, PolicyAction.None },
        { 2, PolicyAction.None },
        { 3, PolicyAction.Examine },
        { 4, PolicyAction.Kill },
        { 5, PolicyAction.KillVeto }
    };

    private static readonly Dictionary<byte, PolicyAction> SevenOrEight = new()
    {
        { 1, PolicyAction.None },
        { 2, PolicyAction.Inspect },
        { 3, PolicyAction.Select },
        { 4, PolicyAction.Kill },
        { 5, PolicyAction.KillVeto }
    };

    private static readonly Dictionary<byte, PolicyAction> NineOrTen = new()
    {
        { 1, PolicyAction.Inspect },
        { 2, PolicyAction.Inspect },
        { 3, PolicyAction.Select },
        { 4, PolicyAction.Kill },
        { 5, PolicyAction.KillVeto }
    };

    public static Dictionary<byte, PolicyAction> GetActions(byte playerCount)
    {
        return playerCount switch
        {
            >= 9 => NineOrTen,
            >= 7 => SevenOrEight,
            _ => FiveOrSix
        };
    }
}