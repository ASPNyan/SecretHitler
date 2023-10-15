using SecretHitler.Game.Boards;
using SecretHitler.Game.Player;
using SecretHitler.Game.Victory;

namespace SecretHitler.Game;

/// <summary>
/// A summary of the events that occurred during a round.
/// </summary>
public record RoundSummary
{
    public GamePlayer President { get; }
    public GamePlayer Chancellor { get; }
    public bool FailedElection { get; }
    public Tile[] TilesDrawn { get; } = Array.Empty<Tile>();
    public bool DiscardedFascist { get; }
    public byte EnactedLiberalPolicies { get; }
    public byte EnactedFascistPolicies { get; }
    public bool? EnactedPolicyWasFascist { get; }
    public PolicyAction[]? PolicyActionsActivated { get; }
    public VictorySummary? Victory { get; }
    
    /// <summary>
    /// A summary of the events that occurred during a round. This constructor indicates a failed election.
    /// </summary>
    /// <param name="president">The player who was President</param>
    /// <param name="chancellor">The player who was assigned Chancellor</param>
    public RoundSummary(GamePlayer president, GamePlayer chancellor)
    {
        President = president;
        Chancellor = chancellor;
        FailedElection = true;
    }

    /// <summary>
    /// A summary of the events that occurred during a round.
    /// </summary>
    /// <param name="president">The player that was President</param>
    /// <param name="chancellor">The player that was elected Chancellor</param>
    /// <param name="tilesDrawn">The three tiles drawn by the President</param>
    /// <param name="discardedFascist">Whether the tile the President discarded was Fascist</param>
    /// <param name="enactedPolicyWasFascist">Whether the tile the Chancellor enacted was Fascist</param>
    /// <param name="enactedFascistPolicies">The total enacted Fascist policies after the round</param>
    /// <param name="enactedLiberalPolicies">The total enacted Liberal policies after the round</param>
    /// <param name="policyActionsActivated">
    /// Any <see cref="PolicyAction">Policy Actions</see> activated by the enacted policy.
    /// </param>
    /// <param name="victory">The type of victory that ended the game, null if the game did not end.</param>
    public RoundSummary(GamePlayer president, GamePlayer chancellor, Tile[] tilesDrawn, bool discardedFascist,
        bool? enactedPolicyWasFascist, byte enactedFascistPolicies, byte enactedLiberalPolicies, 
        PolicyAction[] policyActionsActivated,
        VictorySummary? victory = null)
    {
        President = president;
        Chancellor = chancellor;
        TilesDrawn = tilesDrawn;
        DiscardedFascist = discardedFascist;
        EnactedPolicyWasFascist = enactedPolicyWasFascist;
        EnactedFascistPolicies = enactedFascistPolicies;
        EnactedLiberalPolicies = enactedLiberalPolicies;
        PolicyActionsActivated = policyActionsActivated;
        Victory = victory;
    }
}