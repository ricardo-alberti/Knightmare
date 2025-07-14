internal static class Zobrist
{
    public static readonly ulong[,,] PieceSquare = new ulong[6, 2, 64];
    public static readonly ulong SideToMove;

    static Zobrist()
    {
        Random rng = new Random(123456);
        for (int p = 0; p < 6; p++)
        {
            for (int s = 0; s < 2; s++)
            {
                for (int sq = 0; sq < 64; sq++)
                {
                    byte[] buffer = new byte[8];
                    rng.NextBytes(buffer);
                    PieceSquare[p, s, sq] = BitConverter.ToUInt64(buffer, 0);
                }
            }
        }

        byte[] sideBuf = new byte[8];
        rng.NextBytes(sideBuf);
        SideToMove = BitConverter.ToUInt64(sideBuf, 0);
    }
}

