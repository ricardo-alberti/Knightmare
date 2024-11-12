using Knightmare.Views;
using Knightmare.Moves;
using Knightmare.Boards;
using Knightmare.DTO;

internal sealed class Game
{
    private readonly View view;

    public Game()
    {
        view = new View();
    }

    public void Start()
    {
        Robot whiteBot = new(PlayerSide.White);
        Robot blackBot = new(PlayerSide.Black);

        MoveTree movetree = new();
        CalculationResponse calculation = new();
        Board board = Board.Create(AppSettings.Instance.InitialPosition);

        view.PrintBoard(board, calculation);

        while (true)
        {
            calculation = whiteBot.Calculate(board, AppSettings.Instance.WhiteLevel);
            board = whiteBot.Play(calculation.BestMove(), board);
            view.PrintBoard(board, calculation);

            calculation = blackBot.Calculate(board, AppSettings.Instance.BlackLevel);
            board = blackBot.Play(calculation.BestMove(), board);
            view.PrintBoard(board, calculation);
        }
    }
}
