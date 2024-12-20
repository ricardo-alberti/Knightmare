using Knightmare.Views;
using Knightmare.Boards;

internal sealed class Game
{
    private readonly View view;
    private readonly Player white;
    private readonly Player black;
    public Board ChessBoard { get; set; }

    public Game(Player _white, Player _black, string _initialPosition)
    {
        view = new View();
        white = _white;
        black = _black;
        ChessBoard = Board.Create(_initialPosition);
    }

    public void Start()
    {
        view.PrintBoard(ChessBoard);

        while (true)
        {
            ChessBoard = white.Play(ChessBoard);
            view.PrintBoard(ChessBoard);

            ChessBoard = black.Play(ChessBoard);
            view.PrintBoard(ChessBoard);
        }
    }
}
