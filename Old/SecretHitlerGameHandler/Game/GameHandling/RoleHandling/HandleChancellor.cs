using SecretHitlerBackend.Player;
using SecretHitlerBackend.Player.Handling;
using SecretHitlerBackend.Player.Role;

namespace SecretHitlerGameHandler.Game.GameHandling.RoleHandling;

public static class HandleChancellor
{
    public static void SelectChancellor(this Game game, byte chancellorId, Func<bool[]> vote)
    {
        if (game.GetPresident().PlayerId == chancellorId || game.PreviousChancellor == chancellorId) return;

        Player? player = game.Players.GetPlayer(chancellorId);
        if (player == null) return;
        player.PlayerData.Role.Assign(PlayerRole.ProposedChancellor);

        bool success = CalculateVotes(vote());

        player.PlayerData.Role.Assign(success ? PlayerRole.Chancellor : PlayerRole.Default);
    }

    public static bool CalculateVotes(IReadOnlyCollection<bool> votes)
    {
        double percentage = 0;
        foreach (bool vote in votes)
        {
            if (vote) percentage++;
        }

        percentage /= votes.Count;
        if (percentage > votes.Count / 2d) return true;
        return false;
    }

    internal static Player GetChancellor(this Game game)
    {
        foreach (var player in game.Players.Where(player => player.PlayerData.Role.PlayerRole == PlayerRole.Chancellor))
        {
            return player;
        }

        throw new MissingExpectedValue("Missing Expected \"PlayerRole.President\" Value while Searching All Players In-Game.");
    }
}