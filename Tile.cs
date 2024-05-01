public class Tile
{
    private readonly Point position;
    private readonly ChessPiece piece;

    public Tile() : this(new Piece(), new Point(0, 0))
    {

    }

    public Tile(Point _point) : this(new Piece(), _point)
    {

    }

    public Tile(ChessPiece _piece, Point _point)
    {
        position = _point;
        piece = _piece;
    }

    public ChessPiece Piece()
    {
        return piece;
    }

    public Tile SetPiece(ChessPiece _piece)
    {
        return new Tile(_piece, position);
    }

    public Point Position()
    {
        return position;
    }

    public string Print()
    {
        return piece.Shape();
    }
}

