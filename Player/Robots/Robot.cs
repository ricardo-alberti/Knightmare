using Knightmare.Algorithm;
using Knightmare.Moves;
using Knightmare.Boards;
using Knightmare.DTO;

internal class Robot : Player
{
    private readonly PlayerSide side;
    private readonly TreeSearch search;
    private readonly int level;

    public Robot(PlayerSide _side, int _level) : this(_side, new MinimaxAlphaBeta(), _level) { }
    public Robot(PlayerSide _side, TreeSearch _search, int _level) : base(_side)
    {
        side = _side;
        search = _search;
        level = _level;
    }

    public CalculationResponse Calculate(Board _position)
    {
        Dictionary<int, MoveTree> moveMap = new Dictionary<int, MoveTree>();
        Robot me = new Robot(Side(), level);
        Robot enemy = new Robot(EnemySide(), level);
        Board position = _position.Copy();

        MoveTree movetree = search.BestTree(position, me, enemy, level);
        Node root = movetree.Root();

        CalculationResponse response = new CalculationResponse(
                search.TotalMoves, 
                movetree, 
                root.Value(), 
                search.ElapsedTime, 
                root.eval
        );

        return response;
    }

    override protected Move FindMove(Board _board)
    {
        return this.Calculate(_board).BestMove();
    }
}
