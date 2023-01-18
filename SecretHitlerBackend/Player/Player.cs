using SecretHitlerBackend.Player.Membership.Party;
using SecretHitlerBackend.Player.Membership.Person;
using SecretHitlerBackend.Player.Role;

namespace SecretHitlerBackend.Player;

public class Player
{
    public readonly PlayerData PlayerData;
    public bool Dead = false;
    public byte PlayerId;

    internal Player(PlayerRole Role, PartyMembership PartyMembership, PersonEnum Person, byte PlayerId)
    {
        PlayerData = new PlayerData(Role, PartyMembership, Person);
        this.PlayerId = PlayerId;
    }

    public override string ToString()
    {
        string Output = "Player:\n" +
                              "{\n" +
                             $"  PlayerId: {PlayerId},\n" +
                             $"  Dead: \"{Dead}\",\n" +
                             $"  {PlayerData}\n" +
                              "}\n";
        return Output;
    }
}