public class Board
{
    public ulong WhitePawns { get; set; }
    public ulong WhiteRooks { get; set; }
    public ulong WhiteKnights { get; set; }
    public ulong WhiteBishops { get; set; }
    public ulong WhiteQueens { get; set; }
    public ulong WhiteKing { get; set; }

    public ulong BlackPawns { get; set; }
    public ulong BlackRooks { get; set; }
    public ulong BlackKnights { get; set; }
    public ulong BlackBishops { get; set; }
    public ulong BlackQueens { get; set; }
    public ulong BlackKing { get; set; }

    public ulong ZobristHash;

    public Board()
    {
        WhitePawns = 0x000000000000FF00;
        BlackPawns = 0x00FF000000000000;
    }

    public void MakeMove(int ply)
    {

    }

    /*
    public ulong Hash()
    {
        ulong hash = 0;

        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                Piece? p = Tiles[y, x].TilePiece;
                if (p != null)
                {
                    int index = p.PieceIndex();
                    int side = p.Side == PlayerSide.White ? 0 : 1;
                    int squareIndex = y * 8 + x;
                    hash ^= Zobrist.PieceSquare[index, side, squareIndex];
                }
            }
        }

        if (SideToMove == PlayerSide.White)
        {
            hash ^= Zobrist.SideToMove;
        }

        return hash;
    }
    */
}
