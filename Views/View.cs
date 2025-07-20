using Knightmare.Boards;
using Knightmare.Moves;

namespace Knightmare.Views
{
    internal class View
    {
        private readonly MoveView moveView;

        public View()
        {
            moveView = new MoveView();
        }

        public void PrintBoard(Board _board)
        {
            Console.WriteLine("    A B C D E F G H");
            Console.WriteLine("   -----------------");

            for (int i = 0; i < 8; i++)
            {
                Console.Write($"{8 - i} |");

                for (int j = 0; j < 8; ++j)
                {
                    PrintTile(_board.Tile(j, i));
                }

                Console.WriteLine(" |");
            }

            Console.WriteLine("   -----------------");
            Console.WriteLine("FEN: ");
            Console.WriteLine(BoardParser.FEN(_board));
        }

        public void PrintMove(Move _move)
        {
            moveView.Print(_move);
        }

        private void PrintTile(Tile _tile)
        {
            if (_tile.TilePiece == null)
            {
                Console.Write(" _");
                return;
            }

            Console.Write($" {_tile.TilePiece.Shape}");
        }
    }
}
