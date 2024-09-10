using Knightmare.Views;
using Knightmare.Moves;
using Knightmare.Sets;
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
        DefaultPiecesSet pieces = new DefaultPiecesSet();
        Board board = new Board(pieces.White(), pieces.Black());
        Robot whiteBot = new Robot(PlayerSide.White);
        Robot blackBot = new Robot(PlayerSide.Black);
        MoveTree movetree = new MoveTree();
        CalculationResponse calculation = new CalculationResponse();

        board.SetPieces(board.WhitePieces(), board.BlackPieces());
        view.PrintBoard(board, new CalculationResponse());

        while (true)
        {
            calculation = whiteBot.Calculate(board, 3);
            board = whiteBot.Play(calculation.BestMove(), board);
            view.PrintBoard(board, calculation);

            calculation = blackBot.Calculate(board, 1);
            board = blackBot.Play(calculation.BestMove(), board);
            view.PrintBoard(board, calculation);
        }
    }
}
