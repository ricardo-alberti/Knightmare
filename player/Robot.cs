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

        Dictionary<int, MoveTree> movetrees = moveTrees(position, me, enemy, new Dictionary<int, MoveTree>(), level);
        movetrees.Values.First().Print(movetrees.Values.First().Root());
        Console.Read();

        return Side() == 1 ? movetrees[movetrees.Keys.Max()] : movetrees[movetrees.Keys.Min()];
    }

    public Dictionary<int, MoveTree> moveTrees(Board _position, Robot me, Robot enemy, Dictionary<int, MoveTree> movetrees, int level)
    {
        List<ChessPiece> pieces = _position.SidePieces(me.Side()).Values.ToList();

        foreach (ChessPiece piece in pieces)
        {
            Board copy = _position.Copy();
            List<Move> moveRange = piece.MoveRange(copy);

            foreach (Move move in moveRange)
            {
                copy = _position.Copy();
                Board newPosition = copy.Update(move);
                Node node = new Node(move);

                MoveTree treeRoot = new MoveTree(node);
                MoveTree movetree = moveTree(treeRoot, newPosition, me, enemy, level, treeRoot.Root());

                movetrees[newPosition.Evaluation()] = movetree;
            }
        }

        return movetrees;
    }

    private MoveTree moveTree(MoveTree _movetree, Board _position, Robot me, Robot enemy, int level, Node _root)
    {
        if (level == 0)
        {
            return _movetree;
        }

        List<ChessPiece> pieces = _position.SidePieces(me.Side()).Values.ToList();
        Board newPosition = _position;
        Node node = new Node();

        foreach (ChessPiece piece in pieces)
        {
            Board copy = _position.Copy();
            List<Move> moveRange = piece.MoveRange(copy);

            foreach (Move move in moveRange)
            {
                copy = _position.Copy();
                newPosition = copy.Update(move);
                node = new Node(move);

                _movetree = _movetree.Insert(node, _root);
            }
        }

        _movetree = moveTree(_movetree, newPosition, enemy, me, level - 1, node);
        return _movetree;
    }

    public Move MoveToPlay(MoveTree moves)
    {
        return moves.Root().Value();
    }
}
