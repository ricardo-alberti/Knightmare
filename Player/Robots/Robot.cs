using Knightmare.Algorithm;
using Knightmare.Moves;
using Knightmare.Boards;

internal class Robot : Player
{
    private readonly TreeSearch search;
    private readonly int level;

    public Robot(int _level) : this(new MinimaxAlphaBeta(), _level) { }
    public Robot(TreeSearch _search, int _level)
    {
        search = _search;
        level = _level;
    }

    override protected Move FindMove(Board _board)
    {
        return search.BestTree(_board, level).Value!;
    }
}
