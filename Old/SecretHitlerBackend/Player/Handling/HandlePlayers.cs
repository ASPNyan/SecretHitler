using SecretHitlerBackend.Player.Membership.Party;
using SecretHitlerBackend.Player.Membership.Person;
using SecretHitlerBackend.Player.Role;

namespace SecretHitlerBackend.Player.Handling;

public static class GetPlayers
{
    private static readonly List<byte> UsedPlayerIds = new();

    public static Player? GetPlayer(this IEnumerable<Player> players, byte playerId)
    {
        return players.FirstOrDefault(player => player.PlayerId == playerId);
    }
    
    public static void AddFascists(this ICollection<Player> players, double playerCount)
    {
        int fascists = (int)Math.Round(playerCount / 2) - 1;
        
        for (int I = 0; I < fascists; I++)
        {
            byte random = (byte)new Random().Next(1, (int)playerCount);
            while (UsedPlayerIds.Contains(random)) random = (byte)new Random().Next(1, (int)playerCount + 1);
            PlayerRole role = (PlayerRole)random;
            if (!Enum.IsDefined(typeof(PlayerRole), (int)random))
            {
                role = PlayerRole.Default;
            }

            Player player = I == 1 ? new Player(role, PartyMembership.Fascist, PersonEnum.Hitler, random) : new Player(role, PartyMembership.Fascist, PersonEnum.Fascist, random);
            
            players.Add(player);
            UsedPlayerIds.Add(random);
            Console.WriteLine(UsedPlayerIds.ListToString());
        }
    }

    public static void AddLiberals(this ICollection<Player> players, double playerCount)
    {
        int fascists = (int)Math.Round(playerCount / 2) - 1;
        
        for (int I = 0; I < playerCount - fascists; I++)
        {
            byte random = (byte)new Random().Next(1, (int)playerCount);
            while (UsedPlayerIds.Contains(random)) random = (byte)new Random().Next(1, (int)playerCount + 1);
            PlayerRole role = (PlayerRole)random;
            if (!Enum.IsDefined(typeof(PlayerRole), (int)random))
            {
                role = PlayerRole.Default;
            }

            Player player = new(role, PartyMembership.Liberal, PersonEnum.Liberal, random);

            players.Add(player);
            UsedPlayerIds.Add(random);
            Console.WriteLine(UsedPlayerIds.ListToString());
        }
    }

    private static string ListToString<T>(this IEnumerable<T> list)
    {
        string output = string.Empty;
        bool first = true;
        foreach (T item in list)
        {
            if (first)
            {
                output += item!.ToString();
                first = false;
                continue;
            }

            output += $", {item!}";
        }

        return output;
    }
}
