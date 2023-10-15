using SecretHitlerBackend.Player;
using SecretHitlerBackend.Player.Handling;
using SecretHitlerBackend.Player.Role;

namespace SecretHitlerGameHandler.Game.GameHandling.RoleHandling;

public static class HandlePresident
{
    public static void RotatePresident(this Game game)
    {
        Player currentPresident = game.GetPresident();
        byte presidentId = currentPresident.PlayerId;
        
        Player? nextPresident = null;

        while (nextPresident == null)
        {
            foreach (var player in game.Players.Where(player => player.PlayerId == presidentId % game.PlayerCount + 1))
            {
                nextPresident = game.Players.GetPlayer(presidentId);
            }

            presidentId++;
        }

        currentPresident.PlayerData.Role.Assign(PlayerRole.Default);
        nextPresident.PlayerData.Role.Assign(PlayerRole.President);
    }

    public static void SelectPresident(this Game game, byte presidentId)
    {
        if (game.GetPresident().PlayerId == presidentId || game.President == presidentId) return;

        Player? player = game.Players.GetPlayer(presidentId);

        player?.PlayerData.Role.Assign(PlayerRole.President);
    }
    
    internal static Player GetPresident(this Game game)
    {
        foreach (var player in game.Players.Where(player => player.PlayerData.Role.PlayerRole == PlayerRole.President))
        {
            return player;
        }

        throw new MissingExpectedValue("Missing Expected \"PlayerRole.President\" Value while Searching All Players In-Game.");
    }
}

internal class MissingExpectedValue : Exception
{
    private const string MainErrorMessage = "An Error Has Occurred: An Expected Value was Missing when Required!";
    internal MissingExpectedValue(string message = "") : base(MainErrorMessage + "\n" + message)
    {
    }
}