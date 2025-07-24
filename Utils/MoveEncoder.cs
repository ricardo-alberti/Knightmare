public static class MoveEncoder
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

        int promotion = 0;
        int flag = MoveFlag.None;

        if (moveStr.Length == 5)
        {
            flag = MoveFlag.Promotion;

            promotion = moveStr[4] switch
            {
                'n' => PieceIndex.Knight,
                'b' => PieceIndex.Bishop,
                'r' => PieceIndex.Rook,
                'q' => PieceIndex.Queen,
                _ => throw new ArgumentException("Invalid promotion piece")
            };
        }

        return Encode(from, to, promotion, flag);
    }

    public static int Encode(int from, int to, int promotion = PieceIndex.Null, int flags = 0)
    {
        return (from & 0x3F)
            | ((to & 0x3F) << 6)
            | ((promotion & 0xF) << 12)
            | ((flags & 0xF) << 16);
    }

    public static int FromSquare(int move) => move & 0x3F;
    public static int ToSquare(int move) => (move >> 6) & 0x3F;
    public static int Promotion(int move) => (move >> 12) & 0xF;
    public static int Flags(int move) => (move >> 16) & 0xF;
}
