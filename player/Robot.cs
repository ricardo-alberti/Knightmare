class Robot : Player
{
    private readonly int side;
    private readonly Board board;
    private readonly MoveTree possibleMoves;

    public Robot Update(MoveTree _possibleMoves)
    {
        return new Robot(side, board, _possibleMoves);
    }

    public Robot(int _side, Board _chessBoard, MoveTree _movetree) : base(_side, _chessBoard)
    {
        side = _side;
        possibleMoves = _movetree;
        board = _chessBoard;
    }

    private MoveTree Moves()
    {
        return possibleMoves;
    }

    public ChessPiece Piece(Dictionary<Point, ChessPiece> pieces, int id)
    {

        ChessPiece piece = new Piece();

        foreach (var pair in pieces)
        {
            if (pair.Value.Id() == id)
            {
                return pair.Value;
            }
        }
        throw new Exception("NOT PIECE WITH THIS ID");
    }

    public Move MoveToPlay(Board chessBoard, int id)
    {
        Dictionary<Point, ChessPiece> pieces = chessBoard.SidePieces(side);
        ChessPiece piece = Piece(pieces, id);

        List<Move> moverange = piece.MoveRange(chessBoard);

        return moverange.ElementAt(0);
    }
}
