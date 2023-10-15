using System.Text.RegularExpressions;
using SecretHitlerBackend.Player.Handling;

namespace SecretHitlerBackend;

public static partial class Program
{
    public static void Main()
    {
        int playerCount = GetPlayerCount();
        while (playerCount < 5) playerCount = GetPlayerCount();
        List<Player.Player> players = new();
        players.AddLiberals(playerCount);
        players.AddFascists(playerCount);
        foreach (Player.Player player in players) Console.WriteLine($"{player}");
        Console.ReadKey();
    }

    private static int GetPlayerCount()
    {
        Regex isDigit = new(@"\d{1,2}");
        string? input = null;
        while (string.IsNullOrEmpty(input) || !isDigit.IsMatch(input))
        {
            Console.WriteLine("Please Input the Number of Players\nThere Must be no Less than 5 or More than 10 Players");
            input = Console.ReadLine();
            Console.Clear();
        }
        return int.Parse(input);
    }
}