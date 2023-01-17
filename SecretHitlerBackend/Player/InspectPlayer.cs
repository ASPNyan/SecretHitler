using SecretHitlerBackend.Player.Membership.Party;

namespace SecretHitlerBackend.Player;

public static class InspectPlayer
{
    public static PartyMembership? Inspect(this Player Player)
    {
        if (Player.Dead) return null;
        return Player.PlayerData.Party.PartyMembership;
    }
}