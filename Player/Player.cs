using Knightmare.Moves;
using Knightmare.Boards;

abstract class Player
{
    public Player() { }

    public Move Play(Board _board)
    {
        Move move = FindMove(_board);
        move.Execute(_board);
        return move;
    }

    abstract protected Move FindMove(Board _board);
}
