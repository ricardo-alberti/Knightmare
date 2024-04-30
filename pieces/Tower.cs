class Tower : ChessPiece
{
    public Tower(Point position, int side, int id)
        : base(id, 'T', side == 1 ? " \u2656" : " \u265C", position, new int[,] { { -1, 0 }, { 1, 0 }, { 0, 1 }, { 0, -1 } }, side)
    {

    }
}
