using SecretHitlerBackend.Player;
using SecretHitlerBackend.Player.Role;

namespace SecretHitlerGameHandler.Game.GameHandling.RoleHandling;

public static class HandlePresident
{
    public static void RotatePresident(this Game Game)
    {
        Player CurrentPresident = Game.GetPresident();
        Player? NextPresident = null;
        foreach (var Player in Game.Players.Where(Player => Player.PlayerId == ++CurrentPresident.PlayerId % Game.PlayerCount))
        {
            NextPresident = Player;
        }

        if (NextPresident == null) throw new MissingExpectedValue($"Missing Expected Player After Player with Id {CurrentPresident.PlayerId}.");
        NextPresident.PlayerData.Role.Assign(PlayerRole.President);
        CurrentPresident.PlayerData.Role.Assign(PlayerRole.Default);
    }
    
    internal static Player GetPresident(this Game Game)
    {
        foreach (var Player in Game.Players.Where(Player => Player.PlayerData.Role.PlayerRole == PlayerRole.President))
        {
            return Player;
        }

        throw new MissingExpectedValue("Missing Expected \"PlayerRole.President\" Value while Searching All Players In-Game.");
    }
}

internal class MissingExpectedValue : Exception
{
    private const string MainErrorMessage = "An Error Has Occurred: An Expected Value was Missing when Required!";
    internal MissingExpectedValue(string Message = "") : base(MainErrorMessage + "\n" + Message)
    {
    }
}