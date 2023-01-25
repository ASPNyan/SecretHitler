using SecretHitlerBackend.Boards.FascistBoard;
using SecretHitlerBackend.Boards.LiberalBoard;
using SecretHitlerBackend.Player;
using SecretHitlerBackend.Policies;

namespace SecretHitlerGameHandler.Game;

public abstract class Game
{
    protected internal string GameName;
    protected internal readonly uint GameId;
    protected internal byte PlayerCount;
    protected internal readonly List<Player> Players = new();
    protected internal readonly LiberalBoard LiberalBoard = new();
    protected internal FascistBoard FascistBoard = new(0);
    protected internal readonly Policies Policies = new();
    protected internal byte President = 0;
    protected internal byte PreviousChancellor = 0;
    protected internal Dictionary<byte, string> PregamePlayers = new();

    public Game(string? GameName = null)
    {
        GameId = GenerateGameId();
        this.GameName = GameName ?? GameId.ToString();
    }

    private static uint GenerateGameId()
    {
        uint ThirtyBits = (uint) new Random().Next(1 << 30);
        uint TwoBits = (uint)new Random().Next(1 << 2);
        return ThirtyBits << 2 | TwoBits;
    }
}