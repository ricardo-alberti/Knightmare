using Knightmare.Algorithm;
using Knightmare.Moves;
using Knightmare.Boards;
using Knightmare.DTO;

internal class Robot : Player
{
    private readonly PlayerSide side;
    private readonly Dictionary<string, MoveTree> possiblePositions;

    public Robot() : this(0, new Dictionary<string, MoveTree>()) { }

    public Robot(PlayerSide _side) : this(_side, new Dictionary<string, MoveTree>()) { }

    public Robot(PlayerSide _side, Dictionary<string, MoveTree> _positions) : base(_side)
    {
        side = _side;
        possiblePositions = _positions;
    }

    public CalculationResponse Calculate(Board _position, int level)
    {
        Dictionary<int, MoveTree> moveMap = new Dictionary<int, MoveTree>();
        Robot me = new Robot(Side());
        Robot enemy = new Robot(EnemySide());
        Board position = _position.Copy();
        DFS dfs = new DFS(new SimpleEvaluation());
        BFS bfs = new BFS(new SimpleEvaluation());

        MoveTree movetree = dfs.Execute(position, me, enemy, level);
        Node root = movetree.Root();

        CalculationResponse response = new CalculationResponse(
                dfs.TotalMoves, 
                movetree, 
                root.Value(), 
                dfs.ElapsedTime, 
                root.eval
        );

        return response;
    }
}
