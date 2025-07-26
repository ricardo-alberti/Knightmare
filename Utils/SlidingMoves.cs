internal static class SlidingMoves
{
    public static readonly int[] RookDirections = { 8, -8, 1, -1 };
    public static readonly int[] BishopDirections = { 7, 9, -7, -9 };
    public static readonly int[] QueenDirections = { 7, 9, -7, -9, 8, -8, 1, -1 };

    public static IEnumerable<(int from, int to)> GenerateMoves(Board position, ulong pieces, ulong occupancy, bool isWhite, int[] directions)
    {
        foreach (int from in Bitboard.GetSetBits(pieces))
        {
            foreach (int dir in directions)
            {
                int to = from;
                while (true)
                {
                    to += dir;
                    if (!IsOnBoard(from, to, dir)) break;

                    ulong toBB = 1UL << to;
                    if ((occupancy & toBB) != 0)
                    {
                        if ((toBB & GetEnemyPieces(position, isWhite)) != 0)
                            yield return (from, to);
                        break;
                    }

                    yield return (from, to);
                }
            }
        }
    }

    private static bool IsOnBoard(int from, int to, int dir)
    {
        if (to < 0 || to >= 64) return false;

        int fromRank = from / 8;
        int fromFile = from % 8;
        int toRank = to / 8;
        int toFile = to % 8;

        int rankDiff = toRank - fromRank;
        int fileDiff = toFile - fromFile;

        return dir switch
        {
            1 or -1 => fromRank == toRank,
            8 or -8 => true,
            9 or -9 => Math.Abs(rankDiff) == Math.Abs(fileDiff) && (rankDiff == fileDiff),
            7 or -7 => Math.Abs(rankDiff) == Math.Abs(fileDiff) && (rankDiff == -fileDiff),
            _ => false
        };
    }

    private static ulong GetEnemyPieces(Board position, bool isWhite)
    {
        return isWhite ? position.BlackPieces : position.WhitePieces;
    }
}
