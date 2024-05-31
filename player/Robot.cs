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

    public MoveTree Calculate(Board _position, int level)
    {
        Dictionary<int, MoveTree> moveMap = new Dictionary<int, MoveTree>();
        Robot me = new Robot(Side(), _position, new MoveTree(), "robot1");
        Robot enemy = new Robot(Side() == 1 ? 0 : 1, _position, new MoveTree(), "robot2");
        Board position = _position.Copy();
        Dictionary<int, MoveTree> movetrees = moveTrees(position, me, enemy, level);

        return movetrees.First().Value;
    }

    public Dictionary<int, MoveTree> moveTrees(Board _position, Robot me, Robot enemy, int level, Dictionary<int, MoveTree> movetrees = null)
    {
        if (level == 0)
        {
            return movetrees;
        }

        foreach (ChessPiece piece in _position.SidePieces(me.Side()).Values.ToList())
        {
            List<Move> moveRange = piece.MoveRange(_position);

            if (piece.MoveRange(_position).Count == 0) continue;

            foreach (Move move in moveRange)
            {
                _position = _position.Update(move);
                move.UpdateEval(_position.Evaluate());

                Node node = new Node(move);
                MoveTree movetree = new MoveTree();
                movetree = movetree.Insert(node);

                if (_position.GameOver())
                {
                    return movetrees;
                }
            }
        }

        return movetrees;
    }

    public Move MoveToPlay(MoveTree moves)
    {
        return moves.Root().Value();
    }
}
