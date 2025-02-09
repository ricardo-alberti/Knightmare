using Knightmare.Views;
using Knightmare.Boards;

internal sealed class Game
{
    private readonly View view;
    private readonly Player chessBot;
    public Board ChessBoard { get; set; }

    public Game()
    {
        view = new View();
        chessBot = new Robot(5);
        ChessBoard = Board.Create();
    }

    public void Start()
    {
        Console.WriteLine("id name Knightmare");
        Console.WriteLine("id author Ricardo Alberti");
        Console.WriteLine("uciok");

        while (true)
        {
            string? input = Console.ReadLine();
            if (input == null) continue;

            if (input == "uci")
            {
                Console.WriteLine("id name Knightmare");
                Console.WriteLine("id author Ricardo Alberti");
                Console.WriteLine("uciok");
            }
            else if (input.StartsWith("isready"))
            {
                Console.WriteLine("readyok");
            }
            else if (input.StartsWith("position"))
            {
                ChessBoard = Board.CreateUCI(input);
            }
            else if (input.StartsWith("go"))
            {
                view.PrintMove(chessBot.Play(ChessBoard));
                view.PrintBoard(ChessBoard);
            }
        }
    }
}
