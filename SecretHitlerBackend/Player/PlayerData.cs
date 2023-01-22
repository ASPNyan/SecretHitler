using SecretHitlerBackend.Player.Membership.Party;
using SecretHitlerBackend.Player.Membership.Person;

namespace SecretHitlerBackend.Player;

public class PlayerData
{
    public string Username;
    public Role.Role Role;
    public Party Party;
    public Person Person;

    public PlayerData(string Username, Role.Role Role, Party Party, Person Person)
    {
        this.Username = Username;
        this.Role = Role;
        this.Party = Party;
        this.Person = Person;
    }

    public PlayerData(string Username, Role.PlayerRole Role, PartyMembership PartyMembership, PersonEnum Person)
    {
        this.Username = Username;
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