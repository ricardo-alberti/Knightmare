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

    public Board Play(Move _move, Board _position)
    {
        if (!_move.ValidPlayer(this))
        {
            throw new Exception("Invalid Player Move - player tried to move opposite side piece");
        }

        Board newPosition = _position.Update(_move);

        return newPosition;
    }
}
