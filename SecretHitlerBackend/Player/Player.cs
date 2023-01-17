using SecretHitlerBackend.Player.Membership.Party;
using SecretHitlerBackend.Player.Membership.Person;
using SecretHitlerBackend.Player.Role;

namespace SecretHitlerBackend.Player;

public abstract class Player
{
    public readonly PlayerData PlayerData;
    public bool Dead = false;
    public byte PlayerId;

    protected Player(PlayerRole Role, PartyMembership PartyMembership, PersonEnum Person, byte PlayerId)
    {
        PlayerData = new PlayerData(Role, PartyMembership, Person);
        this.PlayerId = PlayerId;
    }
}