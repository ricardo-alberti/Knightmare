using System.Numerics;

public static class Bitboard
{
    public static int BitScanForward(ulong bb) => BitOperations.TrailingZeroCount(bb);
    public static int BitScanReverse(ulong bb) => 63 - BitOperations.LeadingZeroCount(bb);

    public static IEnumerable<int> GetSetBits(ulong bb)
    {
        while (bb != 0)
        {
            int index = BitScanForward(bb);
            yield return index;
            bb &= bb - 1;
        }
    }
}
