using System.Numerics;

internal static class SlidingMoves
{
    public static readonly int[] RookDirections = { 8, -8, 1, -1 };
    public static readonly int[] BishopDirections = { 7, 9, -7, -9 };
    public static readonly int[] QueenDirections = { 7, 9, -7, -9, 8, -8, 1, -1 };

    public static ulong GetQueenAttacks(int square, ulong occupancy)
    {
        return GetRookAttacks(square, occupancy) | GetBishopAttacks(square, occupancy);
    }

    public static ulong GetBishopAttacks(int square, ulong occupancy)
    {
        int relevantBits = MagicBitboard.BishopRelevantBits[square];
        int shift = 64 - relevantBits;
        ulong blockers = occupancy & MagicBitboard.BishopMasks[square];
        ulong index = (blockers * MagicBitboard.BishopMagics[square]) >> shift;
        return MagicBitboard.BishopAttacks[square][index];
    }

    public static ulong GetRookAttacks(int square, ulong occupancy)
    {
        int relevantBits = MagicBitboard.RookRelevantBits[square];
        int shift = 64 - relevantBits;
        ulong blockers = occupancy & MagicBitboard.RookMasks[square];
        ulong index = (blockers * MagicBitboard.RookMagics[square]) >> shift;

        return MagicBitboard.RookAttacks[square][index];
    }
}
