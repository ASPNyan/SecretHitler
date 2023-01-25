using SecretHitlerBackend.Player;
using SecretHitlerBackend.Player.Handling;
using SecretHitlerBackend.Player.Role;

namespace SecretHitlerGameHandler.Game.GameHandling.RoleHandling;

public static class HandleChancellor
{
    public static void SelectChancellor(this Game Game, byte ChancellorId, Func<bool[]> Vote)
    {
        if (Game.GetPresident().PlayerId == ChancellorId || Game.PreviousChancellor == ChancellorId) return;

        Player? Player = Game.Players.GetPlayer(ChancellorId);
        if (Player == null) return;
        Player.PlayerData.Role.Assign(PlayerRole.ProposedChancellor);

        bool Success = CalculateVotes(Vote());

        Player.PlayerData.Role.Assign(Success ? PlayerRole.Chancellor : PlayerRole.Default);
    }

    public static bool CalculateVotes(IReadOnlyCollection<bool> Votes)
    {
        double Percentage = 0;
        foreach (bool Vote in Votes)
        {
            if (Vote) Percentage++;
        }

        Percentage /= Votes.Count;
        if (Percentage > Votes.Count / 2d) return true;
        return false;
    }

    internal static Player GetChancellor(this Game Game)
    {
        foreach (var Player in Game.Players.Where(Player => Player.PlayerData.Role.PlayerRole == PlayerRole.Chancellor))
        {
            return Player;
        }

        throw new MissingExpectedValue("Missing Expected \"PlayerRole.President\" Value while Searching All Players In-Game.");
    }
}