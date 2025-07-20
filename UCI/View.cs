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
                    Console.WriteLine(" #");
                }

                Console.WriteLine(" |");
            }

            Console.WriteLine("   -----------------");
            Console.WriteLine("FEN: ");
            //Console.WriteLine(BoardParser.FEN(_board));
        }
    }
}
