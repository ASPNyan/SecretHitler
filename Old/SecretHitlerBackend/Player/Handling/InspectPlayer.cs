using SecretHitlerBackend.Player.Membership.Party;

namespace SecretHitlerBackend.Player.Handling;

public static class InspectPlayer
{
    public static PartyMembership? Inspect(this Player player)
    {
        if (player.Dead) return null;
        return player.PlayerData.Party.PartyMembership;
    }
}