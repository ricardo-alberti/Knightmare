class Knight : ChessPiece
{
    public Knight(Point position, int side, int id)
            : base(id, 'N', side == 1 ? " \u2658" : " \u265E", position, new int[,] { { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 }, { 1, 2 }, { -1, 2 }, { 1, -2 }, { -1, -2 } }, side)
    {

    }
}
