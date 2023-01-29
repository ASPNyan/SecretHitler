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

    public Game(IReadOnlyDictionary<uint, Game> Games, string? GameName = null)
    {
        GameId = GenerateGameId();
        while (Games.ContainsKey(GameId)) GameId = GenerateGameId();
        this.GameName = GameName ?? GameId.ToString();
    }

    public Game(uint GameId, string? GameName = null)
    {
        this.GameId = GameId;
    }

    private static uint GenerateGameId()
    {
        uint ThirtyBits = (uint) new Random().Next(1 << 30);
        uint TwoBits = (uint)new Random().Next(1 << 2);
        return ThirtyBits << 2 | TwoBits;
    }
}