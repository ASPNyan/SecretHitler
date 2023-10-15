namespace SecretHitler.Game;

public class Tiles
{
    private List<Tile> _storage; // This should always have 3 elements.

    public Tiles()
    {
        _storage = new List<Tile>();
        FillStorage();
    }

    private readonly Random _tileRng = new();
    private int GetRng() => _tileRng.Next(1, 18); // 18 because the upper bound is exclusive, whereas the lower is inclusive

    private void FillStorage()
    {
        while (_storage.Count < 3)
        {
            _storage.Add(new Tile(GetRng()));
        }
    }

    public Tile DrawSingle()
    {
        Tile output = _storage[0];

        _storage[0] = _storage[1];
        _storage[1] = _storage[2];
        _storage[2] = new Tile(GetRng());

        return output;
    }

    public Tile[] DrawThree(bool replace = true) // replace is not always wanted, see 
    {
        Tile[] output = _storage.ToArray();

        if (replace)
        {
            _storage = new List<Tile>();
            FillStorage();
        }

        return output;
    }
}