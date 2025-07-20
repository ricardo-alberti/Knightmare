using System.Numerics;

public static class Bitboard
{
    public const ulong Rank4 = 0x00000000FF000000;
    public const ulong Rank2 = 0x000000000000FF00;
    public const ulong Rank5 = 0x000000FF00000000;
    public const ulong Rank7 = 0x00FF000000000000;

    public const ulong FileA = 0x0101010101010101;
    public const ulong FileH = 0x8080808080808080;

    public static int BitScanForward(ulong bb) => BitOperations.TrailingZeroCount(bb);
    public static int BitScanReverse(ulong bb) => 63 - BitOperations.LeadingZeroCount(bb);

    public static IEnumerable<int> GetSetBits(ulong bb)
    {
        while (bb != 0)
        {
            int index = BitScanForward(bb);
            yield return index;
            bb &= bb - 1; // clear LSB
        }
    }
}
