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
        int relevantBits = Magic.BishopRelevantBits[square];
        int shift = 64 - relevantBits;
        ulong blockers = occupancy & Magic.BishopMasks[square];
        ulong index = (blockers * Magic.BishopMagics[square]) >> shift;
        return Magic.BishopAttacks[square][index];
    }

    public static ulong GetRookAttacks(int square, ulong occupancy)
    {
        int relevantBits = Magic.RookRelevantBits[square];
        int shift = 64 - relevantBits;
        ulong blockers = occupancy & Magic.RookMasks[square];
        ulong index = (blockers * Magic.RookMagics[square]) >> shift;

        return Magic.RookAttacks[square][index];
    }
}
