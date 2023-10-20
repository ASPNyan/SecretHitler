using SecretHitler.Game.Boards;
using SecretHitler.Game.Player;

namespace SecretHitler.Game;

public class Round(in GameController game)
{
    public GameController Game { get; } = game;

    private GamePlayer President { get; } = game.Players[game.CurrentPresident];
    private GamePlayer _chancellor = null!;
    private bool _electionPassed = true;

    private Tile[] _tilesDrawn = null!;
    private Tile _presidentDiscardedTile;
    private Tile _chancellorSelectedTile;
    private bool _veto;
    private PolicyAction[] _actions = null!;

    public async Task StartRound()
    {
        _chancellor = await President.SelectPlayerTrigger(SelectPlayerTriggerType.NominateChancellor);

        bool continuePlay = await Game.NominateChancellor(_chancellor, GetVotes);

        switch (continuePlay, Game.IsGameOver)
        {
            case (false, true): // hitler wins by election
                EndGame();
                return;
            case (false, false): // failed election
                _electionPassed = false;
                EndRound();
                return;
            case (true, _): // successful election
                await PostElection();
                return;
        }

        async Task<byte> GetVotes()
        {
            byte playersVoted = 0;
            byte yesVotes = 0;

            foreach (GamePlayer player in Game.Players.Values) player.VoteTrigger(OnVote);

            while (playersVoted < Game.AlivePlayers) await Task.Delay(500);

            return yesVotes;

            void OnVote(bool yesVote)
            {
                playersVoted++;
                if (yesVote) yesVotes++;
            }
        }
    }

    public async Task PostElection()
    {
        _tilesDrawn = Game.DrawTiles();

        await President.SelectTileDiscardTrigger(_tilesDrawn, false, out byte presidentTileDiscardIndex);

        Tile[] chancellorTiles = presidentTileDiscardIndex switch
        {
            0 => _tilesDrawn.Skip(1).ToArray(),
            1 => new[] { _tilesDrawn[0], _tilesDrawn[2] },
            _ => _tilesDrawn[..2]
        };

        _presidentDiscardedTile = _tilesDrawn[presidentTileDiscardIndex];

        bool requestVeto = await _chancellor
            .SelectTileDiscardTrigger(chancellorTiles, Game.VetoActive, out byte chancellorTileDiscardIndex);

        if (requestVeto)
        {
            _veto = await President.VetoTrigger();

            if (_veto)
            {
                Game.ProgressElectionTracker(); // veto is an inactive government
                EndRound();
                return;
            }
        }

        _chancellorSelectedTile = chancellorTiles[chancellorTileDiscardIndex ^ 1];
        
        _actions = Game.PlayTile(_chancellorSelectedTile);
        
        Game.ResetElectionTracker(); // active government resets tracker

        if (Game.IsGameOver)
        {
            EndGame();
            return;
        }

        EndRound();
    }

    public void EndRound()
    {
        Game.RoundSummaries.Add(CreateRoundSummary());
        Game.ProgressPresident();
    }

    public void EndGame()
    {
        Game.RoundSummaries.Add(CreateRoundSummary());
        foreach (GamePlayer player in Game.Players.Values)
            player.GameOverTrigger(Game.Victory!.Value);
    }

    public RoundSummary CreateRoundSummary() =>
        _electionPassed
            ? new RoundSummary(President, _chancellor)
            : new RoundSummary(President, _chancellor, _tilesDrawn, _presidentDiscardedTile.IsFascist,
                _veto ? null : _chancellorSelectedTile.IsFascist, Game.EnactedFascistPolicies,
                Game.EnactedLiberalPolicies, _actions, Game.Victory);
}