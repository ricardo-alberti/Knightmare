using Knightmare.Algorithm;
using Knightmare.Moves;
using Knightmare.Boards;
using Knightmare.DTO;

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

    public MoveStats Calculate(Board _position)
    {
        Board position = _position;
        MoveTree movetree = search.BestTree(position, level);
        Node root = movetree.Root;
        MoveStats response = new MoveStats(
                search.TotalMoves,
                movetree,
                root.Value,
                search.ElapsedTime,
                root.Eval
        );

        return response;
    }

    public List<MoveTree> GetInitialMoveTrees(Board _position, int _depth)
    {
        return search.GetInitialMoveTrees(_position, _depth);
    }

    override protected MoveStats FindMove(Board _board)
    {
        return this.Calculate(_board);
    }
}
