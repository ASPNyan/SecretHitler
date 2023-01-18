using SecretHitlerBackend.Player.Membership.Party;
using SecretHitlerBackend.Player.Membership.Person;

namespace SecretHitlerBackend.Player;

public class PlayerData
{
    public Role.Role Role;
    public Party Party;
    public Person Person;

    public PlayerData(Role.Role Role, Party Party, Person Person)
    {
        this.Role = Role;
        this.Party = Party;
        this.Person = Person;
    }

    public PlayerData(Role.PlayerRole Role, PartyMembership PartyMembership, PersonEnum Person)
    {
        this.Role = new Role.Role(Role);
        Party = new Party(PartyMembership);
        this.Person = new Person(Person);
    }

    public override string ToString()
    {
        string Output = "PlayerData:\n" +
                        "  {\n" +
                       $"    Person: {Person.PersonEnum},\n" +
                       $"    Party: {Party.PartyMembership},\n" +
                       $"    Role: {Role.PlayerRole}\n" +
                        "  }";
        return Output;
    }
}