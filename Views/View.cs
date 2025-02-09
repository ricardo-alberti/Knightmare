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

        public void PrintBoard(Board _board, MoveStats _stats = null, bool _FEN = true)
        {
            Console.WriteLine("     H G F E D C B A");
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

            if (_stats != null)
            {
                Console.WriteLine($"Time elapsed: {_stats.ElapsedTime()}");
                Console.WriteLine($"Calculated moves: {_stats.CalculatedMoves()}");
                Console.WriteLine($"Eval: {_stats.Evaluation()}");
                moveTreeView.Print(_stats.MoveTree());
                moveView.Print(_stats.Move());
            }

            if (_FEN == true)
            {
                Console.WriteLine("FEN: ");
                Console.WriteLine(_board.FEN());
            }
        }

        public void PrintMove(MoveStats _stats)
        {
            moveView.Print(_stats.Move());
        }

        private void PrintTile(Tile _tile)
        {
            if (_tile.Piece() == null)
            {
                Console.Write(" _");
                return;
            }

            Console.Write(_tile.Piece().Shape());
        }
    }
}
