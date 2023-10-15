using SecretHitlerBackend.Boards.FascistBoard;
using SecretHitlerBackend.Boards.LiberalBoard;
using SecretHitlerBackend.Player;
using SecretHitlerBackend.Policies;

namespace SecretHitlerGameHandler.Game;

public class Game
{
    public string GameName;
    public readonly uint GameId;
    public byte PlayerCount;
    protected internal readonly List<Player> Players = new();
    protected internal readonly LiberalBoard LiberalBoard = new();
    protected internal FascistBoard FascistBoard = new(0);
    protected internal readonly Policies Policies = new();
    protected internal byte President = 0;
    protected internal byte PreviousChancellor = 0;
    protected internal readonly Dictionary<byte, string> PregamePlayers = new();

    public Game(IReadOnlyDictionary<uint, Game> games, string? gameName = null)
    {
        GameId = GenerateGameId();
        while (games.ContainsKey(GameId)) GameId = GenerateGameId();
        this.GameName = gameName ?? GameId.ToString();
    }

    public Game(uint gameId, string? gameName = null)
    {
        this.GameId = gameId;
    }

    private static uint GenerateGameId()
    {
        uint thirtyBits = (uint) new Random().Next(1 << 30);
        uint twoBits = (uint)new Random().Next(1 << 2);
        return thirtyBits << 2 | twoBits;
    }
}