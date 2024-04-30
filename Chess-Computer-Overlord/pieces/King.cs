class King : ChessPiece
{
    public King(Point position, int side, int id)
        : base(id, 'K', side == 1 ? " \u2654" : " \u265A", position, new int[,] { { 0, -1 }, { 0, 1 }, { 1, 0 }, { 1, 1 }, { -1, 0 }, { -1, -1 }, { 1, -1 }, { -1, -1 } }, side)
    {

    }
}
