using Knightmare.Boards;
using Knightmare.DTO;

namespace Knightmare.Views
{
    internal class View
    {
        private readonly MoveView moveView;
        private readonly MoveTreeView moveTreeView;

        public View()
        {
            moveView = new MoveView();
            moveTreeView = new MoveTreeView();
        }

        public void PrintBoard(Board _board, CalculationResponse _calculation, bool _FEN = true)
        {
            Console.Clear();

            Console.WriteLine("     A B C D E F G H");
            Console.WriteLine("    -----------------");

            for (int i = 0; i < 8; i++)
            {
                Console.Write($" {i + 1} |");

                for (int j = 0; j < 8; ++j)
                {
                    PrintTile(_board.Tile(j, i));
                }

                Console.WriteLine(" |");
            }

            Console.WriteLine("    -----------------");

            Console.WriteLine($"Calculated Moves: {_calculation.CalculatedMovesTotal()}");
            Console.WriteLine($"Elapsed Time: {_calculation.ElapsedTime()} ms");
            Console.WriteLine($"Evaluation: {_calculation.Evaluation()}");

            moveView.Print(_calculation.BestMove());
            moveTreeView.Print(_calculation.MoveTree());

            if (_FEN == true)
            {
                Console.Write("FEN: ");
                Console.Write(_board.FEN());
            }
        }

        private void PrintTile(Tile _tile)
        {
            Console.Write(_tile.Piece().Shape());
        }
    }
}
