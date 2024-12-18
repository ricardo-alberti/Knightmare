using Knightmare.Moves;
using Knightmare.Boards;
using Knightmare.DTO;

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

    public MoveStats Play(Board _board)
    {
        MoveStats stats = FindMove(_board);
        Move move = stats.Move();

        if (!move.ValidPlayer(this))
        {
            throw new Exception("Invalid Player Move");
        }

        _board.Update(move);

        return stats;
    }

    abstract protected MoveStats FindMove(Board _board);
}
