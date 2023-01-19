using SecretHitlerBackend.Player.Membership.Party;
using SecretHitlerBackend.Victory;

namespace SecretHitlerBackend.Boards.FascistBoard;

public class FascistBoard : Board
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

    public FascistBoard(byte PlayerCount)
    {
        BoardActions = Actions.GetActions(PlayerCount);
    }
}