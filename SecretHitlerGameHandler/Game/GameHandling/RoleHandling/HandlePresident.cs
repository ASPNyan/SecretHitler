using SecretHitlerBackend.Player;
using SecretHitlerBackend.Player.Handling;
using SecretHitlerBackend.Player.Role;

namespace SecretHitlerGameHandler.Game.GameHandling.RoleHandling;

public static class HandlePresident
{
    public static void RotatePresident(this Game Game)
    {
        Player CurrentPresident = Game.GetPresident();
        byte PresidentId = CurrentPresident.PlayerId;
        
        Player? NextPresident = null;

        while (NextPresident == null)
        {
            foreach (var Player in Game.Players.Where(Player => Player.PlayerId == PresidentId % Game.PlayerCount + 1))
            {
                NextPresident = Game.Players.GetPlayer(PresidentId);
            }

            PresidentId++;
        }

        CurrentPresident.PlayerData.Role.Assign(PlayerRole.Default);
        NextPresident.PlayerData.Role.Assign(PlayerRole.President);
    }

    public static void SelectPresident(this Game Game, byte PresidentId)
    {
        if (Game.GetPresident().PlayerId == PresidentId || Game.President == PresidentId) return;

        Player? Player = Game.Players.GetPlayer(PresidentId);

        Player?.PlayerData.Role.Assign(PlayerRole.President);
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