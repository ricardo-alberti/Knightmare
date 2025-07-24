public static class SlidingMoves
{
    private static readonly int[] RookDirections = { 8, -8, 1, -1 };
    private static readonly int[] BishopDirections = { 7, 9, -7, -9 };

    public static IEnumerable<(int from, int to)> GenerateBishopMoves(Board position, ulong bishops, ulong occupancy, bool isWhite)
    {
        foreach (int from in Bitboard.GetSetBits(bishops))
        {
            foreach (int dir in BishopDirections)
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

    public static IEnumerable<(int from, int to)> GenerateRookMoves(Board position, ulong rooks, ulong occupancy, bool isWhite)
    {
        foreach (int from in Bitboard.GetSetBits(rooks))
        {
            foreach (int dir in RookDirections)
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

        int fromRank = from / 8, fromFile = from % 8;
        int toRank = to / 8, toFile = to % 8;

        return dir switch
        {
            1 or -1 => fromRank == toRank,
            8 or -8 => true,
            7 => (toFile == fromFile - 1) && (toRank == fromRank + 1),
            9 => (toFile == fromFile + 1) && (toRank == fromRank + 1),
            -7 => (toFile == fromFile + 1) && (toRank == fromRank - 1),
            -9 => (toFile == fromFile - 1) && (toRank == fromRank - 1),
            _ => false
        };
    }

    private static ulong GetEnemyPieces(Board position, bool isWhite)
    {
        return isWhite ? position.BlackPieces() : position.WhitePieces();
    }
}
