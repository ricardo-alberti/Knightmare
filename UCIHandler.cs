using Knightmare.Boards;
using Knightmare.Moves;
using Knightmare.Views;

internal sealed class UCIHandler
{
    private bool Debug;
    private readonly View view;
    private readonly Robot chessBot;
    private Board chessBoard;

    public UCIHandler()
    {
        view = new();
        chessBoard = new();
        chessBot = new Robot(7);
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
                chessBoard = BoardParser.CreateUCI(input);
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
        Move move = chessBot.Play(chessBoard);
        view.PrintMove(move);

        if (Debug)
        {
            Console.WriteLine("Debug Info: Move Statistics");

            view.PrintBoard(chessBoard);
        }
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
