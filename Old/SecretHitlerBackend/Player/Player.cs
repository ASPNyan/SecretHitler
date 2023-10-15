using SecretHitlerBackend.Player.Membership.Party;
using SecretHitlerBackend.Player.Membership.Person;
using SecretHitlerBackend.Player.Role;

namespace SecretHitlerBackend.Player;

public class Player
{
    public readonly PlayerData PlayerData;
    public bool Dead = false;
    public byte PlayerId;

    internal Player(PlayerRole role, PartyMembership partyMembership, PersonEnum person, byte playerId, string username = "")
    {
        PlayerData = new PlayerData(username, role, partyMembership, person);
        this.PlayerId = playerId;
    }

    public override string ToString()
    {
        string output = "Player:\n" +
                              "{\n" +
                             $"  PlayerId: {PlayerId},\n" +
                             $"  Dead: \"{Dead}\",\n" +
                             $"  {PlayerData}\n" +
                              "}\n";
        return output;
    }
}