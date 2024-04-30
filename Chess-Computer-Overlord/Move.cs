public sealed class Move
{
    private readonly ChessPiece piece;
    private readonly Tile initialTile;
    private readonly Tile finalTile;

    public Move() : this(new Tile(new Point(0, 0)), new Tile(new Point(0, 0))) { }

    public Move(Tile _initialTile, Tile _finalTile)
    {
        piece = _initialTile.Piece();
        initialTile = _initialTile.SetPiece(new Piece()); ;
        finalTile = _finalTile.SetPiece(piece.UpdatePosition(_finalTile.Position()));
    }

    public Tile[] Tiles()
    {
        return new Tile[] { initialTile, finalTile };
    }

    public void Print()
    {
        Console.WriteLine($"{piece.Shape()} from ({initialTile.Position().x}, {initialTile.Position().y}) to ({finalTile.Position().x}, {finalTile.Position().y})");
        Console.Read();
    }
}
