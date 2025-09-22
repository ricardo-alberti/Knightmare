internal static class MoveEncoder
{
    public static int SquareToIndex(string square)
    {
        int file = square[0] - 'a';
        int rank = square[1] - '1';
        return rank * 8 + file;
    }

    public static int Encode(string moveStr, Board position)
    {
        int from = SquareToIndex(moveStr.Substring(0, 2));
        int to = SquareToIndex(moveStr.Substring(2, 2));

        int promotion = PieceIndex.Null;
        MoveFlags flag = MoveFlags.Quiet;

        if (moveStr.Length == 5)
        {
            flag |= MoveFlags.Promotion;

            promotion = moveStr[4] switch
            {
                'n' => PieceIndex.Knight,
                'b' => PieceIndex.Bishop,
                'r' => PieceIndex.Rook,
                'q' => PieceIndex.Queen,
                _ => throw new ArgumentException("Invalid promotion piece")
            };
        }

        if (position.GetPieceAtSquare(to, !position.WhiteToMove) != PieceIndex.Null) 
        {
            flag |= MoveFlags.Capture;        
        }

        return Encode(from, to, promotion, flag);
    }

    public static int Encode(int from, int to, int promotion = PieceIndex.Null, MoveFlags flags = MoveFlags.Quiet)
    {
        return (from & 0x3F)
            | ((to & 0x3F) << 6)
            | ((promotion & 0xF) << 12)
            | (((int)flags & 0xF) << 16);
    }

    public static int FromSquare(int move) => move & 0x3F;
    public static int ToSquare(int move) => (move >> 6) & 0x3F;
    public static int Promotion(int move) => (move >> 12) & 0xF;
    public static MoveFlags Flags(int move) => (MoveFlags)((move >> 16) & 0xFF);
}

