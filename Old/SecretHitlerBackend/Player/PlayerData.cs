using SecretHitlerBackend.Player.Membership.Party;
using SecretHitlerBackend.Player.Membership.Person;

namespace SecretHitlerBackend.Player;

public class PlayerData
{
    public string Username;
    public Role.Role Role;
    public Party Party;
    public Person Person;

    public PlayerData(string username, Role.Role role, Party party, Person person)
    {
        this.Username = username;
        this.Role = role;
        this.Party = party;
        this.Person = person;
    }

    public PlayerData(string username, Role.PlayerRole role, PartyMembership partyMembership, PersonEnum person)
    {
        this.Username = username;
        this.Role = new Role.Role(role);
        Party = new Party(partyMembership);
        this.Person = new Person(person);
    }

    public override string ToString()
    {
        string output = "PlayerData:\n" +
                        "  {\n" +
                       $"    Person: {Person.PersonEnum},\n" +
                       $"    Party: {Party.PartyMembership},\n" +
                       $"    Role: {Role.PlayerRole}\n" +
                        "  }";
        return output;
    }
}