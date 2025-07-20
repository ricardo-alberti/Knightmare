using Knightmare.Algorithm;
using Knightmare.Moves;
using Knightmare.Boards;

internal class Engine
{
    public TreeSearch Search { get; }
    private int Level { get; }

    public Engine(int _level) : this(new AlphaBeta(), _level) { }
    public Engine(TreeSearch _search, int _level)
    {
        Search = _search;
        Level = _level;
    }

    public Move Play(Board _board)
    {
        Move move = FindMove(_board);
        move.Execute(_board);
        return move;
    }

    protected Move FindMove(Board _board)
    {
        return Search.BestTree(_board, Level).Value!;
    }
}
