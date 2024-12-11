using Knightmare.Moves;
using Knightmare.Boards;

abstract class Player
{
    private PlayerSide side { get; }

    public Player(PlayerSide _side)
    {
        side = _side;
    }

    public PlayerSide Side()
    {
        return side;
    }

    public PlayerSide EnemySide()
    {
        PlayerSide enemySide = PlayerSide.White;

        if (Side() == PlayerSide.White)
        {
            enemySide = PlayerSide.Black;
        }

        return enemySide;
    }

    public Board Play(Board _board)
    {
        Move move = FindMove(_board);

        if (!move.ValidPlayer(this))
        {
            throw new Exception("Invalid Player Move");
        }

        Board newPosition = _board.Update(move);

        return newPosition;
    }

    abstract protected Move FindMove(Board _board);
}
