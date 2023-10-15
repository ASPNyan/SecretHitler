namespace SecretHitler.Game;

public struct Tile
{
    public bool IsFascist { get; }

    internal Tile(int rng)
    {
        IsFascist = rng > 6;
    }
}