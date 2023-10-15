using System.Collections.Concurrent;
using SecretHitler.Game;

namespace SecretHitler;

public class GamesService
{
    private ConcurrentDictionary<Guid, GameController> Games { get; } = new();
}