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

    public void Play(Board _board)
    {
        Move move = FindMove(_board);

        if (!move.ValidPlayer(this))
        {
            throw new Exception("Invalid Player Move");
        }

        _board.Update(move);
    }

    abstract protected Move FindMove(Board _board);
}
