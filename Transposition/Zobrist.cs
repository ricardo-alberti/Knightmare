internal static class Zobrist
{
    public static readonly ulong[,,] PieceSquare = new ulong[2, 6, 64]; // [color, piece, square]
    public static readonly ulong SideToMove;
    public static readonly ulong[] CastlingRights = new ulong[16]; // 4 bits for KQkq
    public static readonly ulong[] EnPassantFile = new ulong[8];   // A-H file

    private static readonly Random rng = new(0xABCDEF); // Fixed seed for reproducibility

    static Zobrist()
    {
        for (int color = 0; color < 2; color++)
        {
            for (int piece = 0; piece < 6; piece++)
            {
                for (int square = 0; square < 64; square++)
                {
                    PieceSquare[color, piece, square] = NextRandom();
                }
            }
        }

        SideToMove = NextRandom();

        for (int i = 0; i < 16; i++)
            CastlingRights[i] = NextRandom();

        for (int i = 0; i < 8; i++)
            EnPassantFile[i] = NextRandom();
    }

    private static ulong NextRandom()
    {
        byte[] bytes = new byte[8];
        rng.NextBytes(bytes);
        return BitConverter.ToUInt64(bytes, 0);
    }
}
