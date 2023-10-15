using SecretHitlerBackend.Boards.FascistBoard;
using SecretHitlerBackend.Player.Handling;
using SecretHitlerGameHandler.Game.GameHandling.RoleHandling;

namespace SecretHitlerGameHandler.Game.GameHandling;

public static class StartGame
{
    public static void Start(this Game game)
    {
        if (game.PlayerCount < 5)
        {
            return;
        }
        game.Players.AddFascists(game.PlayerCount);
        game.Players.AddLiberals(game.PlayerCount);
        game.PreviousChancellor = game.GetChancellor().PlayerId;
        game.FascistBoard.PlacedTiles = 0;
        game.LiberalBoard.PlacedTiles = 0;
        game.FascistBoard = new FascistBoard(game.PlayerCount);
    }
}