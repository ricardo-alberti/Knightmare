using Knightmare.Algorithm;
using Knightmare.Moves;
using Knightmare.Boards;

internal class Robot : Player
{
    public TreeSearch Search { get; }
    private readonly int level;

    public Robot(int _level) : this(new MinimaxAlphaBeta(), _level) { }
    public Robot(TreeSearch _search, int _level)
    {
        Search = _search;
        level = _level;
    }

    override protected Move FindMove(Board _board)
    {
        return Search.BestTree(_board, level).Value!;
    }
}
