internal class BoardView
{
    private readonly BoardParser boardParser;

    public BoardView() 
    {
        boardParser = new BoardParser();
    }

    public void Print(Board board)
    {
        Console.WriteLine("    A B C D E F G H");
        Console.WriteLine("   -----------------");

        for (int rank = 7; rank >= 0; rank--)
        {
            Console.Write($"{rank + 1} |");

            for (int file = 0; file < 8; file++)
            {
                int square = rank * 8 + file;
                char pieceChar = boardParser.GetPieceCharAtSquare(board, square);
                Console.Write($" {pieceChar}");
            }

            Console.WriteLine(" |");
        }

        Console.WriteLine("   -----------------");
    }
}

