class Pawn : ChessPiece
{
    public Pawn(Point position, int side, int id)
        : base(id, 'P', side == 1 ? " \u2659" : " \u265F", position, side == 0 ? new int[,] { { -1, -1 }, { -1, 1 }, { -1, 0 } } : new int[,] { { 1, 1 }, { 1, -1 }, { 1, 0 } }, side)
    {

    }

    public override List<Move> MoveRange(Board _boardPosition)
    {
        List<Move> moveRange = new List<Move>();

        Tile initialTile = _boardPosition.Tile(Position().x, Position().y);
        ChessPiece piece = initialTile.Piece();
        int[,] moveSet = piece.MoveSet();
        int moveset_x, moveset_y, finaltile_x, finaltile_y;

        for (int i = 0; i < moveSet.GetLength(0); ++i)
        {
            moveset_x = moveSet[i, 0];
            moveset_y = moveSet[i, 1];

            finaltile_x = initialTile.Position().x + moveset_x;
            finaltile_y = initialTile.Position().y + moveset_y;

            if (finaltile_x < 0 || finaltile_y < 0 || finaltile_y > 7 || finaltile_x > 7) continue;

            Tile finalTile = _boardPosition.Tile(finaltile_x, finaltile_y);

            if (finalTile.Piece().Side() == initialTile.Piece().Side()) continue;

            Move move = new Move(initialTile, finalTile);
            moveRange.Add(move);

            break;
        }

        return moveRange;
    }
}
