using SecretHitlerBackend.Player.Membership.Party;
using SecretHitlerBackend.Player.Membership.Person;

namespace SecretHitlerBackend.Player;

public class PlayerData
{
    public Role.Role Role;
    public Membership.Party.Party Party;
    public Membership.Person.Person Person;

    public PlayerData(Role.Role Role, Party Party, Person Person)
    {
        this.Role = Role;
        this.Party = Party;
        this.Person = Person;
    }

    public PlayerData(Role.PlayerRole Role, PartyMembership PartyMembership, PersonEnum Person)
    {
        this.Role = new Role.Role(Role);
        this.Party = new Party(PartyMembership);
        this.Person = new Person(Person);
    }
}