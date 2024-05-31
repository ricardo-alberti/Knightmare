public sealed class Move
{
    private readonly ChessPiece piece;
    private readonly ChessPiece captured;
    private readonly Tile initialTile;
    private readonly Tile finalTile;
    private readonly int evaluation;

    public Move() : this(new Tile(new Point(0, 0)), new Tile(new Point(0, 0))) { }

    public Move(Tile _initialTile, Tile _finalTile) : this(_initialTile, _finalTile, 0)
    {

    }

    public Move(Tile _initialTile, Tile _finalTile, int _evaluation)
    {
        piece = _initialTile.Piece();
        captured = _finalTile.Piece();
        initialTile = _initialTile.SetPiece(new Piece());
        finalTile = _finalTile.SetPiece(piece.UpdatePosition(_finalTile.Position()));
        evaluation = _evaluation;
    }

    public Tile[] Tiles()
    {
        return new Tile[2] { initialTile, finalTile };
    }

    public void Print(string prefix = "")
    {
        Console.Write(prefix);
        Console.WriteLine(piece.Side() == 1 ? "White moved: " : "Black moved: ");
        Console.Write(prefix);
        Console.WriteLine($"{piece.Shape()} from ({initialTile.Position().x}, {initialTile.Position().y}) to ({finalTile.Position().x}, {finalTile.Position().y})");
    }

    public void Print(int eval)
    {
        Console.WriteLine(piece.Side() == 1 ? "White moved: " : "Black moved: ");
        Console.WriteLine($"{piece.Shape()} from ({initialTile.Position().x}, {initialTile.Position().y}) to ({finalTile.Position().x}, {finalTile.Position().y})");
        Console.Write("\nEvaluation: ");
        Console.WriteLine(eval);
    }

    public Move Undo()
    {
        return new Move(finalTile, initialTile);
    }

    public int Eval()
    {
        return evaluation;
    }

    public Move UpdateEval(int _evalutation)
    {
        return new Move(initialTile, finalTile, _evalutation);
    }

    public char pieceNotation()
    {
        return piece.Notation();
    }
}
