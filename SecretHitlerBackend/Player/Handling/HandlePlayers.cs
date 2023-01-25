using SecretHitlerBackend.Player.Membership.Party;
using SecretHitlerBackend.Player.Membership.Person;
using SecretHitlerBackend.Player.Role;

namespace SecretHitlerBackend.Player.Handling;

public static class GetPlayers
{
    private static readonly List<byte> UsedPlayerIds = new();

    public static Player? GetPlayer(this IEnumerable<Player> Players, byte PlayerId)
    {
        return Players.FirstOrDefault(Player => Player.PlayerId == PlayerId);
    }
    
    public static void AddFascists(this ICollection<Player> Players, double PlayerCount)
    {
        int Fascists = (int)Math.Round(PlayerCount / 2) - 1;
        
        for (int I = 0; I < Fascists; I++)
        {
            byte Random = (byte)new Random().Next(1, (int)PlayerCount);
            while (UsedPlayerIds.Contains(Random)) Random = (byte)new Random().Next(1, (int)PlayerCount + 1);
            PlayerRole Role = (PlayerRole)Random;
            if (!Enum.IsDefined(typeof(PlayerRole), (int)Random))
            {
                Role = PlayerRole.Default;
            }

            Player Player = I == 1 ? new Player(Role, PartyMembership.Fascist, PersonEnum.Hitler, Random) : new Player(Role, PartyMembership.Fascist, PersonEnum.Fascist, Random);
            
            Players.Add(Player);
            UsedPlayerIds.Add(Random);
            Console.WriteLine(UsedPlayerIds.ListToString());
        }
    }

    public static void AddLiberals(this ICollection<Player> Players, double PlayerCount)
    {
        int Fascists = (int)Math.Round(PlayerCount / 2) - 1;
        
        for (int I = 0; I < PlayerCount - Fascists; I++)
        {
            byte Random = (byte)new Random().Next(1, (int)PlayerCount);
            while (UsedPlayerIds.Contains(Random)) Random = (byte)new Random().Next(1, (int)PlayerCount + 1);
            PlayerRole Role = (PlayerRole)Random;
            if (!Enum.IsDefined(typeof(PlayerRole), (int)Random))
            {
                Role = PlayerRole.Default;
            }

            Player Player = new(Role, PartyMembership.Liberal, PersonEnum.Liberal, Random);

            Players.Add(Player);
            UsedPlayerIds.Add(Random);
            Console.WriteLine(UsedPlayerIds.ListToString());
        }
    }

    private static string ListToString<T>(this IEnumerable<T> List)
    {
        string Output = string.Empty;
        bool First = true;
        foreach (T Item in List)
        {
            if (First)
            {
                Output += Item!.ToString();
                First = false;
                continue;
            }

            Output += $", {Item!}";
        }

        return Output;
    }
}
