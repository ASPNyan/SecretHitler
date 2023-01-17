using SecretHitlerBackend.Player.Membership.Party;
using SecretHitlerBackend.Victory;

namespace SecretHitlerBackend.Boards.FacistBoard;

public class FacistBoard : Board
{
    public Dictionary<byte, PolicyAction> BoardActions;
    public override byte MaxTiles => 6;
    public override PartyMembership Party => PartyMembership.Fascist;

    public override Victory.Victory? AddTile()
    {
        PlacedTiles++;
        if (PlacedTiles >= MaxTiles) return new Victory.Victory(FascistVictory.PolicyEnactment);
        return null;
    }

    public FacistBoard(byte PlayerCount)
    {
        BoardActions = Actions.GetActions(PlayerCount);
    }
}