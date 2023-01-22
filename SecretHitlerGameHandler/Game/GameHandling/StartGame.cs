using SecretHitlerBackend.Boards.FascistBoard;
using SecretHitlerBackend.Player;
using SecretHitlerGameHandler.Game.GameHandling.RoleHandling;

namespace SecretHitlerGameHandler.Game.GameHandling;

public static class StartGame
{
    public static void Start(this Game Game)
    {
        if (Game.PlayerCount < 5)
        {
            return;
        }
        Game.Players.AddFascists(Game.PlayerCount);
        Game.Players.AddLiberals(Game.PlayerCount);
        Game.PreviousChancellor = Game.GetChancellor().PlayerId;
        Game.FascistBoard.PlacedTiles = 0;
        Game.LiberalBoard.PlacedTiles = 0;
        Game.FascistBoard = new FascistBoard(Game.PlayerCount);
    }
}