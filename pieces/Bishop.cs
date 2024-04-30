class Bishop : ChessPiece
{
    public Bishop(Point position, int side, int id)
        : base(id, 'B', side == 1 ? " \u2657" : " \u265D", position, new int[,] { { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } }, side)
    {

    }
}
