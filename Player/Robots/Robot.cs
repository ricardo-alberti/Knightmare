using Knightmare.Algorithm;
using Knightmare.Moves;
using Knightmare.Boards;
using Knightmare.DTO;

internal class Robot : Player
{
    private readonly PlayerSide side;
    private readonly TreeSearch search;

    public Robot(PlayerSide _side) : this(_side, new MinimaxAlphaBeta()) { }
    public Robot(PlayerSide _side, TreeSearch _search) : base(_side)
    {
        side = _side;
        search = _search;
    }

    public CalculationResponse Calculate(Board _position, int level)
    {
        Dictionary<int, MoveTree> moveMap = new Dictionary<int, MoveTree>();
        Robot me = new Robot(Side());
        Robot enemy = new Robot(EnemySide());
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
}
