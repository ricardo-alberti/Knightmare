using Knightmare.Moves;
using Knightmare.Boards;
using Knightmare.DTO;

abstract class Player
{
    public Player() { }

    public MoveStats Play(Board _board)
    {
        MoveStats stats = FindMove(_board);
        Move move = stats.Move();

        move.Execute(_board);

        return stats;
    }

    abstract protected MoveStats FindMove(Board _board);
}
