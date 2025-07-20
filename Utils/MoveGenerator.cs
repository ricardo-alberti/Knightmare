public static class MoveGenerator
{
    public static IEnumerable<(int from, int to)> GenerateWhitePawnMoves(Board board)
    {
        ulong empty = ~(board.WhitePawns | board.WhiteRooks | board.WhiteKnights | board.WhiteBishops |
                        board.WhiteQueens | board.WhiteKing |
                        board.BlackPawns | board.BlackRooks | board.BlackKnights | board.BlackBishops |
                        board.BlackQueens | board.BlackKing);

        ulong pawns = board.WhitePawns;

        // One step forward
        ulong oneStep = (pawns << 8) & empty;

        foreach (int to in Bitboard.GetSetBits(oneStep))
        {
            int from = to - 8;
            yield return (from, to);
        }

        // Two steps forward from rank 2
        ulong rank2Pawns = pawns & Bitboard.Rank2;
        ulong twoSteps = ((rank2Pawns << 8) & empty) << 8 & empty;

        foreach (int to in Bitboard.GetSetBits(twoSteps))
        {
            int from = to - 16;
            yield return (from, to);
        }

        // Captures
        ulong enemy = board.BlackPawns | board.BlackRooks | board.BlackKnights | board.BlackBishops |
                      board.BlackQueens | board.BlackKing;

        // Capture left (from white's POV)
        ulong captureLeft = (pawns << 7) & ~Bitboard.FileH & enemy;
        foreach (int to in Bitboard.GetSetBits(captureLeft))
        {
            int from = to - 7;
            yield return (from, to);
        }

        // Capture right
        ulong captureRight = (pawns << 9) & ~Bitboard.FileA & enemy;
        foreach (int to in Bitboard.GetSetBits(captureRight))
        {
            int from = to - 9;
            yield return (from, to);
        }
    }
}

