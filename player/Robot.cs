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

    private int[] PiecesIds(Dictionary<Point, ChessPiece> pieces)
    {
        int[] ids = new int[pieces.Count];

        for (int i = 0; i < pieces.Count; i++)
        {
            ids[i] = pieces.ElementAt(i).Value.Id();
        }

        return ids;
    }

    public MoveTree Calculate(Board _position, Robot me, Robot enemy, int level)
    {
        MoveTree movetree = new MoveTree();

        int piece_id = PiecesIds(_position.SidePieces(me.Side()))[12];
        ChessPiece piece = Piece(_position.SidePieces(me.Side()), piece_id);

        List<Move> moveRange = piece.MoveRange(_position);
        Move move = moveRange.ElementAt(0);
        Node node = new Node(move);
        movetree = movetree.Insert(node);
        Board newposition = _position.Update(move);

        //enemy
        piece_id = PiecesIds(_position.SidePieces(enemy.Side()))[12];
        piece = Piece(_position.SidePieces(enemy.Side()), piece_id);

        moveRange = piece.MoveRange(newposition);

        foreach (Move enemyMove in moveRange)
        {
            movetree = movetree.Insert(new Node(enemyMove), node);
        }

        return movetree;
    }

    public Move MoveToPlay(MoveTree moves)
    {
        return moves.Root().Value();
    }

    public int Evaluate(Board _position)
    {
        return 0;
    }
}
