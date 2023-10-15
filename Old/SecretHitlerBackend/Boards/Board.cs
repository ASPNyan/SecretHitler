using SecretHitlerBackend.Player.Membership.Party;
using SecretHitlerBackend.Victory;

namespace SecretHitlerBackend.Boards;

public abstract class Board
{
    public virtual byte MaxTiles => 5;
    public byte PlacedTiles;
    public virtual PartyMembership Party => PartyMembership.Liberal;

    public virtual Victory.Victory? AddTile()
    {
        PlacedTiles++;
        if (PlacedTiles >= MaxTiles) return new Victory.Victory(LiberalVictory.PolicyEnactment);
        return null;
    }
}