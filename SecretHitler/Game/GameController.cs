using SecretHitler.Game.Boards;
using SecretHitler.Game.Exceptions;
using SecretHitler.Game.Player;
using SecretHitler.Game.Victory;

namespace SecretHitler.Game;

public class GameController
{
    /// <summary>
    /// <b>WARNING: 1 Based</b>
    /// </summary>
    public byte StartingPlayersCount { get; }
    public Dictionary<byte, GamePlayer> Players { get; }
    public byte MaxLiberalTiles { get; private set; }
    public byte MaxFascistTiles { get; private set; }
    
    private FascistBoard FascistBoard { get; }
    public byte EnactedFascistPolicies => FascistBoard.EnactedPolicies;
    private LiberalBoard LiberalBoard { get; }
    public byte EnactedLiberalPolicies => LiberalBoard.EnactedPolicies;
    
    /// <summary>
    /// <b>CAUTION: This property is 0 based, like the game.</b>
    /// </summary>
    public byte MaxElectionTracker { get; }
    
    #region Class Construction
    
    private GameController(GamePlayer[] startingPlayers, PolicyAction[][] actionGroups, 
        byte maxElectionTracker = 3, byte? maxLiberalTiles = null, byte? maxFascistTiles = null)
    {
        if (startingPlayers.Length < 5) throw new GameCreationException(nameof(startingPlayers), 
            "There must be 5 or more players to create a game, check your ");
        
        StartingPlayersCount = (byte)startingPlayers.Length;
        Players = new Dictionary<byte, GamePlayer>();

        foreach (GamePlayer player in startingPlayers) Players.Add(player.PlayerId, player); // fill player storage

        MaxElectionTracker = maxElectionTracker;

        SetMaxTiles(maxLiberalTiles, maxFascistTiles);

        FascistBoard = new FascistBoard(VictoryTrigger, PolicyActionTrigger,
            HitlerEndGameTrigger, actionGroups, (byte)maxFascistTiles!);

        LiberalBoard = new LiberalBoard(VictoryTrigger, MaxLiberalTiles);
    }

    private void SetMaxTiles(byte? maxLiberalTiles, byte? maxFascistTiles)
    {
        // Ratio of Liberal Tiles to Fascist Tiles where there are less Liberal Tiles.
        const float tileRatio = 0.83333f;
        const byte minLiberalTiles = 5;
        const byte minFascistTiles = 6;
        
        if (maxLiberalTiles is null && maxFascistTiles is null)
        {
            maxFascistTiles = StartingPlayersCount switch
            {
                <= 10 => minFascistTiles,
                _ => (byte)(minFascistTiles + MathF.Ceiling((StartingPlayersCount - 10) / 2f))
            };
        }

        if (maxLiberalTiles is null)
        {
            byte maxFascistTilesValue = maxFascistTiles!.Value;
            
            maxLiberalTiles = (byte)(maxFascistTilesValue * tileRatio); // casting to unsigned numbers is the same as flooring

            MaxLiberalTiles = (byte)maxLiberalTiles;
            MaxFascistTiles = (byte)maxFascistTiles;

            return;
        }

        if (maxFascistTiles is null)
        {
            byte maxLiberalTilesValue = maxLiberalTiles.Value;
            
            maxFascistTiles = (byte)MathF.Ceiling(maxLiberalTilesValue / tileRatio);
            
            MaxLiberalTiles = (byte)maxLiberalTiles;
            MaxFascistTiles = (byte)maxFascistTiles;

            return;
        }
        
        if (maxLiberalTiles < minLiberalTiles) maxLiberalTiles = minLiberalTiles;
        if (maxFascistTiles < minFascistTiles) maxFascistTiles = minFascistTiles;
        
        MaxLiberalTiles = (byte)maxLiberalTiles;
        MaxFascistTiles = (byte)maxFascistTiles;
    }
    
    #endregion
    
    #region Game State

    private bool _endGameActive;
    public bool VetoActive { get; private set; }
    public byte CurrentPresident { get; private set; }
    public byte? NextPresident { get; private set; }
    /// Null when no chancellor is nominated or appointed
    public byte? CurrentChancellor { get; private set; } = null;
    
    public byte AlivePlayers => (byte)Players.Values.Select(p => !p.IsDead).Count();
    
    private byte _electionTracker;

    private byte ElectionTracker
    {
        get => _electionTracker;
        set
        {
            _electionTracker = value;
            if (_electionTracker >= MaxElectionTracker) PlayTile();
        }
    }

    private Tiles Tiles { get; } = new();

    public List<RoundSummary> RoundSummaries { get; } = new();

    public VictorySummary? Victory { get; private set; }
    public bool IsGameOver => Victory is not null;
    
    #endregion

    #region Managing Game State

    public void ResetElectionTracker() => _electionTracker = 0;

    public void ProgressElectionTracker() => ++_electionTracker;
    
    public void ProgressPresident()
    {
        CurrentPresident = NextPresident ?? CurrentPresident++;
        while (Players[CurrentPresident].IsDead) CurrentPresident++;
        NextPresident = null;
        CurrentChancellor = null;
    }
    
    /// <param name="nominee">The nominated Chancellor</param>
    /// <param name="votingMethodReturningYesVotes">
    /// An async <see cref="Func{TResult}">Func&lt;Task&lt;byte&gt;&gt;()</see> returning the amount of "Yes" votes
    /// </param>
    /// <returns>
    /// True when the round should continue. <see cref="IsGameOver"/> should be checked on false.
    /// <see cref="ProgressElectionTracker"/> is called automatically on false.
    /// </returns>
    public async Task<bool> NominateChancellor(GamePlayer nominee, Func<Task<byte>> votingMethodReturningYesVotes)
    {
        if (CurrentChancellor is not null) throw new InvalidOperationException(
            "A new Chancellor cannot be nominated before progressing Presidency; call `ProgressPresident()` " +
            "before this (`NominateChancellor(GamePlayer, Func<Task<byte>>)`) method."
            );
        
        CurrentChancellor = nominee.PlayerId;

        byte yesVotes = await votingMethodReturningYesVotes();

        if (yesVotes > (byte)((AlivePlayers - 1) / 2f))
        {
            if (_endGameActive && Players[CurrentChancellor.Value].PlayerRole is PlayerRole.Hitler)
            {
                VictoryTrigger(new VictorySummary(PlayerRole.Fascist, false));
                return false;
            }
            return true;
        }

        ProgressElectionTracker();
        return false;
    }

    private PolicyAction[] PlayTileToBoard(Tile tile)
    {
        ResetElectionTracker();
        if (!tile.IsFascist)
        {
            LiberalBoard.EnactPolicy();
            return Array.Empty<PolicyAction>();
        }
        return FascistBoard.EnactPolicy();
    }
    
    private void PlayTile()
    {
        var tile = Tiles.DrawSingle();
        
        PlayTileToBoard(tile);
    }

    /// <summary>
    /// Plays the selected tile. This will automatically call <see cref="ResetElectionTracker"/>.
    /// </summary>
    public PolicyAction[] PlayTile(Tile tile) => PlayTileToBoard(tile);
    
    /// <summary>
    /// Draws three tiles and replaces them in storage.
    /// </summary>
    /// <returns></returns>
    public Tile[] DrawTiles()
    {
        Tile[] tiles = Tiles.DrawThree();

        return tiles;
    }

    private void PolicyActionTrigger(PolicyAction[] policies)
    {
        foreach (PolicyAction action in policies)
        {
            switch (action)
            {
                case PolicyAction.PolicyPeek:
                    ActionTrigger(action, ObjectArrayConverter(Tiles.DrawThree(false)));
                    break;
#pragma warning disable CS8974 // Converting method group to non-delegate type
                case PolicyAction.InvestigateLoyalty:
                    ActionTrigger(action, GetPartyMembership);
                    break;
                case PolicyAction.CallSpecialElection:
                    ActionTrigger(action, SetNextPresident);
                    break;
                case PolicyAction.Execute:
                    ActionTrigger(action, ExecutePlayer);
                    break;
#pragma warning restore CS8974 // Converting method group to non-delegate type
                case PolicyAction.Veto:
                    VetoActive = true;
                    break;
                default:
                    break;
            }
        }

        return;
        // Local methods after explicit return.
        void ActionTrigger(PolicyAction action, params object[]? args) =>
            Players[CurrentPresident].ActionTrigger(action, args);

        object[] ObjectArrayConverter<T>(T[] items) where T : notnull 
            => Array.ConvertAll(items, i => i as object);

        PlayerRole GetPartyMembership(GamePlayer gamePlayer) => gamePlayer.PartyMembership;
        
        void SetNextPresident(GamePlayer gamePlayer)
        {
            if (gamePlayer.IsDead) 
                throw new ArgumentException("Next president cannot be set to a dead player.", nameof(gamePlayer));
            
            NextPresident = gamePlayer.PlayerId;
        }

        void ExecutePlayer(GamePlayer gamePlayer) => gamePlayer.Execute();
    }

    private void HitlerEndGameTrigger() => _endGameActive = true;
    
    private void VictoryTrigger(VictorySummary victory) => Victory = victory;

    #endregion

    public sealed class Builder
    {
        
    }
}