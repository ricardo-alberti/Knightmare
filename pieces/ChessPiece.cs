public abstract class ChessPiece
{
    private readonly int id;
    private readonly char notation;
    private readonly string shape;
    private readonly Point position;
    private readonly int[,] moveSet;
    private readonly int side;
    private readonly bool attacked;

    protected ChessPiece(int _id, char _notation, string _shape, Point _position, int[,] _moveSet, int _side, bool _attacked = false)
    {
        id = _id;
        notation = _notation;
        shape = _shape;
        position = _position;
        moveSet = _moveSet;
        side = _side;
        attacked = _attacked;
    }

    public ChessPiece UpdatePosition(Point _position)
    {
        return CreatePiece(this.notation, _position, this.side, id);
    }

    public string Shape()
    {
        return shape;
    }

    public bool Attacked()
    {
        return attacked;
    }

    public char Notation()
    {
        return notation;
    }

    public int Id()
    {
        return id;
    }

    public int Side()
    {
        return side;
    }

    public virtual List<Move> MoveRange(Board _boardPosition)
    {
        List<Move> moveRange = new List<Move>();

        Tile initialTile = _boardPosition.Tile(Position().x, Position().y);
        ChessPiece piece = initialTile.Piece();
        int[,] moveSet = piece.MoveSet();

        int moveset_x, moveset_y, finaltile_x, finaltile_y;
        Tile finalTile = new Tile(new Point());
        Move move = new Move();

        for (int i = 0; i < moveSet.GetLength(0); ++i)
        {
            moveset_x = moveSet[i, 0];
            moveset_y = moveSet[i, 1];

            finaltile_x = initialTile.Position().x + moveset_x;
            finaltile_y = initialTile.Position().y + moveset_y;

            while (finaltile_x >= 0 && finaltile_y >= 0 && finaltile_y <= 7 && finaltile_x <= 7)
            {
                finalTile = _boardPosition.Tile(finaltile_x, finaltile_y);

                if (finalTile.Piece().Side() == initialTile.Piece().Side()) break;

                move = new Move(initialTile, finalTile);
                moveRange.Add(move);

                finaltile_x += moveset_x;
                finaltile_y += moveset_y;
            }
        }

        return moveRange;
    }

    public ChessPiece Promote(char notation)
    {
        return CreatePiece(notation, position, side, id);
    }

    public int[,] MoveSet()
    {
        return moveSet;
    }

    public Point Position()
    {
        return position;
    }

    public static ChessPiece CreatePiece(char notation, Point position, int side, int id)
    {
        switch (notation)
        {
            case 'N':
                return new Knight(position, side, id);
            case 'T':
                return new Tower(position, side, id);
            case 'P':
                return new Pawn(position, side, id);
            case 'Q':
                return new Queen(position, side, id);
            case 'K':
                return new King(position, side, id);
            case 'B':
                return new Bishop(position, side, id);
            default:
                return new Piece();
        }
    }
}
