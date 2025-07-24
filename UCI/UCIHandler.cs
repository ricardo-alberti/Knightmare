internal sealed class UCIHandler
{
    private readonly BoardView boardView;
    private readonly MoveView moveView;
    private readonly AlphaBeta alphaBeta;
    private readonly BoardParser boardParser;

    private bool Debug;
    private Board board;

    public UCIHandler()
    {
        boardView = new();
        moveView = new();
        board = new();
        boardParser = new();
        alphaBeta = new();
    }

    public void ProcessCommand(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return;

        string[] parts = input.Split(' ', 2);
        string command = parts[0];
        string arguments = parts.Length > 1 ? parts[1] : string.Empty;

        switch (command)
        {
            case "uci":
                Console.WriteLine("id name Knightmare");
                Console.WriteLine("id author Ricardo Alberti");
                Console.WriteLine("uciok");
                break;

            case "isready":
                Console.WriteLine("readyok");
                break;

            case "position":
                board = boardParser.CreateBoardFromUCI(input);
                break;

            case "go":
                ExecuteGoCommand();
                break;

            case "debug":
                HandleDebug(arguments);
                break;

            default:
                if (Debug)
                {
                    Console.WriteLine($"unknown command: {input}");
                }

                break;
        }
    }

    private void ExecuteGoCommand()
    {
        List<Node> tree = new();
        int rootIndex = alphaBeta.BestTree(board, 7, int.MinValue, int.MaxValue, board.WhiteToMove, tree);
        Node root = tree[rootIndex];
        board.MakeMove(root.Move);
        moveView.Print(root.Move);

        if (Debug)
        {
            boardView.Print(board);
        }
    }

    private void HandleDebug(string argument)
    {
        if (argument == "on")
        {
            Console.WriteLine("debug mode enabled");
            Debug = true;
        }
        else if (argument == "off")
        {
            Console.WriteLine("debug mode disabled");
            Debug = false;
        }
        else
        {
            Console.WriteLine("use 'debug on' or 'debug off'");
        }
    }
}
