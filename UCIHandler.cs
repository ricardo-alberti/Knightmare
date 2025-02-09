using Knightmare.Boards;
using Knightmare.DTO;
using Knightmare.Views;

internal sealed class UCIHandler
{
    private bool Debug;
    private readonly View view;
    private readonly Player chessBot;
    private Board chessBoard;

    public UCIHandler()
    {
        view = new View();
        chessBot = new Robot(5);
        chessBoard = Board.Create();
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
                chessBoard = Board.CreateUCI(input);
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
                    Console.WriteLine($"Unknown command: {input}");
                }
                break;
        }
    }

    private void ExecuteGoCommand()
    {
        MoveStats stats = chessBot.Play(chessBoard);

        if (Debug)
        {
            Console.WriteLine("Debug Info: Move Statistics");
            view.PrintBoard(chessBoard, stats);
        }

        view.PrintMove(stats);
    }

    private void HandleDebug(string argument)
    {
        if (argument == "on")
        {
            Console.WriteLine("Debug mode enabled");
            Debug = true;
        }
        else if (argument == "off")
        {
            Console.WriteLine("Debug mode disabled");
            Debug = false;
        }
        else
        {
            Console.WriteLine("Unknown debug command. Use 'debug on' or 'debug off'.");
        }
    }
}
