using System.Diagnostics.CodeAnalysis;
using SecretHitler.Game.Boards;
using SecretHitler.Game.Victory;

namespace SecretHitler.Game.Player;

public class GamePlayer(byte playerId, PlayerRole playerRole, Action<PolicyAction, object[]?> actionTrigger, 
    Action<Action<bool>> voteTrigger, Func<SelectPlayerTriggerType, Task<GamePlayer>> selectPlayerTrigger, 
    SelectTileDiscard selectTileDiscardTrigger, Func<Task<bool>> vetoTrigger, Action<VictorySummary> gameOverTrigger)
{
    public byte PlayerId { get; } = playerId;
    public PlayerRole PlayerRole { get; } = playerRole;
    public PlayerRole PartyMembership => PlayerRole is PlayerRole.Hitler ? PlayerRole.Fascist : PlayerRole;
    public Action<PolicyAction, object[]?> ActionTrigger { get; } = actionTrigger;
    public Action<Action<bool>> VoteTrigger { get; } = voteTrigger;
    public Func<SelectPlayerTriggerType, Task<GamePlayer>> SelectPlayerTrigger { get; } = selectPlayerTrigger;
    public SelectTileDiscard SelectTileDiscardTrigger { get; } = selectTileDiscardTrigger;
    public Func<Task<bool>> VetoTrigger { get; } = vetoTrigger;
    public Action<VictorySummary> GameOverTrigger { get; } = gameOverTrigger;
    public bool IsDead { get; private set; }

    public void Execute() => IsDead = true;
    
}

/// <returns>True if Veto is requested, otherwise false.</returns>
public delegate Task<bool> SelectTileDiscard(Tile[] tiles, bool canVeto, out byte discardedIndex);