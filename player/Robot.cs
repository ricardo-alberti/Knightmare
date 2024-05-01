class Robot : Player
{
    private readonly int side;
    private readonly Board board;
    private readonly string name;
    private readonly MoveTree possibleMoves;

    public Robot() : this(0, new Board(), new MoveTree(), "robot") { }

    public Robot(int _side, Board _chessBoard, MoveTree _movetree, string _name) : base(_side, _chessBoard, _name)
    {
        side = _side;
        possibleMoves = _movetree;
        board = _chessBoard;
        name = _name;
    }

    private MoveTree Moves()
    {
        return possibleMoves;
    }

    private ChessPiece Piece(Dictionary<Point, ChessPiece> pieces, int id)
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

    private int[] PiecesIds(Dictionary<Point, ChessPiece> pieces)
    {
        int[] ids = new int[pieces.Count];

        for (int i = 0; i < pieces.Count; i++)
        {
            ids[i] = pieces.ElementAt(i).Value.Id();
        }

        return ids;
    }

    public MoveTree Calculate(Board _position)
    {
        MoveTree movetree = new MoveTree();
        Robot me = new Robot(1, board, possibleMoves, "me");
        Robot enemy = new Robot(0, board, possibleMoves, "enemy");

        int piece_id = PiecesIds(_position.SidePieces(side))[12];
        List<Move> moverange = Piece(_position.SidePieces(side), piece_id).MoveRange(_position);

        for (int i = 0; i < moverange.Count; i++)
        {
            movetree = movetree.Insert(moverange.ElementAt(i));
        }

        return movetree;
    }

    public Move MoveToPlay(MoveTree moves)
    {
        return moves.Root().Value();
    }
}
