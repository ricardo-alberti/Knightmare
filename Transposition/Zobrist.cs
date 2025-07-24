public static class Zobrist
{
    public static ulong[,,] PieceSquare = new ulong[6, 2, 64]; 
    public static ulong WhiteToMove;

    static Zobrist()
    {
        Random rng = new Random(123456);
        for (int p = 0; p < 6; p++)
            for (int c = 0; c < 2; c++)
                for (int s = 0; s < 64; s++)
                    PieceSquare[p, c, s] = RandomULong(rng);

        WhiteToMove = RandomULong(rng);
    }

    private static ulong RandomULong(Random rng)
    {
        byte[] buffer = new byte[8];
        rng.NextBytes(buffer);
        return BitConverter.ToUInt64(buffer, 0);
    }
}

