using System.Text.RegularExpressions;
using SecretHitlerBackend.Player;

namespace SecretHitlerBackend;

public static partial class Program
{
    public static void Main()
    {
        int PlayerCount = GetPlayerCount();
        while (PlayerCount < 5) PlayerCount = GetPlayerCount();
        List<Player.Player> Players = new();
        Players.AddLiberals(PlayerCount);
        Players.AddFascists(PlayerCount);
        foreach (Player.Player Player in Players) Console.WriteLine($"{Player}");
        Console.ReadKey();
    }

    private static int GetPlayerCount()
    {
        Regex IsDigit = DigitRegex();
        string? Input = null;
        while (string.IsNullOrEmpty(Input) || !IsDigit.IsMatch(Input))
        {
            Console.WriteLine("Please Input the Number of Players\nThere Must be no Less than 5 or More than 10 Players");
            Input = Console.ReadLine();
            Console.Clear();
        }
        return int.Parse(Input);
    }

    [GeneratedRegex(@"\d{1,2}")]
    private static partial Regex DigitRegex();
}