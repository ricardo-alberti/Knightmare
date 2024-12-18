using Knightmare.Boards;
using Knightmare.Moves;
using Knightmare.Views;
using Knightmare.DTO;

class Human : Player
{
    public Human(PlayerSide _side) : base(_side)
    {

    }

    override protected MoveStats FindMove(Board _board)
    {
        View view = new();
        Move move = new();
        Tile first = new();
        Tile last = new();

        Console.WriteLine();
        Console.Write("Input: ");
        string? input = Console.ReadLine();
        input = input?.ToLower();

        if (string.IsNullOrEmpty(input) || input.Length != 4)
        {
            return FindMove(_board);
        }

        if (input == "undo")
        {
            _board.Undo();
            _board.Undo();

            view.PrintBoard(_board, new MoveStats(new Move()));
            return FindMove(_board);
        }

        int firstX = input[0] - 'a';
        int firstY = input[1] - '1';
        int lastX = input[2] - 'a';
        int lastY = input[3] - '1';

        if (firstX > 7 || firstX < 0 || firstY > 7 || firstY < 0
            || lastX > 7 || lastX < 0 || lastY > 7 || lastY < 0)
        {
            return FindMove(_board);
        }

        first = _board.Tile(firstX, firstY);
        last = _board.Tile(lastX, lastY);
        move = new Move(first, last);

        return new MoveStats(move);
    }
}
