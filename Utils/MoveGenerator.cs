public static class MoveGenerator
{
    public static readonly ulong[] KnightAttacks = new ulong[64];
    public static readonly ulong[] KingAttacks = new ulong[64];
    public static readonly ulong[] WhitePawnAttacks = new ulong[64];
    public static readonly ulong[] BlackPawnAttacks = new ulong[64];

    static MoveGenerator()
    {
        for (int sq = 0; sq < 64; sq++)
        {
            KnightAttacks[sq] = GenerateKnightAttacks(sq);
            KingAttacks[sq] = GenerateKingAttacks(sq);
            WhitePawnAttacks[sq] = GenerateWhitePawnAttacks(sq);
            BlackPawnAttacks[sq] = GenerateBlackPawnAttacks(sq);
        }
    }

    public static List<int> GenerateMoves(Board board)
    {
        var moves = new List<int>();
        bool isWhite = board.WhiteToMove;
        ulong ownPawns = isWhite ? board.WhitePawns : board.BlackPawns;
        ulong ownKnights = isWhite ? board.WhiteKnights : board.BlackKnights;
        ulong ownBishops = isWhite ? board.WhiteBishops : board.BlackBishops;
        ulong ownRooks = isWhite ? board.WhiteRooks : board.BlackRooks;
        ulong ownQueens = isWhite ? board.WhiteQueens : board.BlackQueens;
        ulong ownKing = isWhite ? board.WhiteKing : board.BlackKing;
        ulong ownPieces = isWhite ? board.WhitePieces() : board.BlackPieces();
        ulong enemyPieces = isWhite ? board.BlackPieces() : board.WhitePieces();
        ulong occupancy = ownPieces | enemyPieces;

        foreach (int from in Bitboard.GetSetBits(ownKnights))
        {
            ulong attacks = MoveGenerator.KnightAttacks[from] & ~ownPieces;
            foreach (int to in Bitboard.GetSetBits(attacks))
            {
                MoveFlags flags = ((1UL << to) & enemyPieces) != 0 ? MoveFlags.Capture : 0; 
                moves.Add(MoveEncoder.Encode(from, to, 0, flags));
            }
        }

        foreach (int from in Bitboard.GetSetBits(ownKing))
        {
            ulong attacks = MoveGenerator.KingAttacks[from] & ~ownPieces;
            foreach (int to in Bitboard.GetSetBits(attacks))
            {
                MoveFlags flags = ((1UL << to) & enemyPieces) != 0 ? MoveFlags.Capture : 0; 

                if (isWhite && from == 60)
                {
                    if (to == 62) flags |= MoveFlags.KingCastle;
                    else if (to == 58) flags |= MoveFlags.QueenCastle;
                }
                else if (!isWhite && from == 4)
                {
                    if (to == 6) flags |= MoveFlags.KingCastle;
                    else if (to == 2) flags |= MoveFlags.QueenCastle;
                }

                moves.Add(MoveEncoder.Encode(from, to, 0, flags));
            }
        }

        foreach (int from in Bitboard.GetSetBits(ownPawns))
        {
            int dir = isWhite ? 8 : -8;
            int to = from + dir;

            if (to >= 0 && to < 64 && ((1UL << to) & occupancy) == 0)
            {
                if ((isWhite && to >= 56) || (!isWhite && to < 8))
                {
                    moves.Add(MoveEncoder.Encode(from, to, PieceIndex.Queen));
                    moves.Add(MoveEncoder.Encode(from, to, PieceIndex.Rook));
                    moves.Add(MoveEncoder.Encode(from, to, PieceIndex.Bishop));
                    moves.Add(MoveEncoder.Encode(from, to, PieceIndex.Knight));
                }
                else
                {
                    moves.Add(MoveEncoder.Encode(from, to));
                }
            }

            ulong attacks = isWhite
                ? MoveGenerator.WhitePawnAttacks[from]
                : MoveGenerator.BlackPawnAttacks[from];
            attacks &= enemyPieces;

            foreach (int toCap in Bitboard.GetSetBits(attacks))
            {
                MoveFlags flags = MoveFlags.Capture; 

                if ((isWhite && toCap >= 56) || (!isWhite && toCap < 8))
                {
                    moves.Add(MoveEncoder.Encode(from, toCap, PieceIndex.Queen, flags));
                    moves.Add(MoveEncoder.Encode(from, toCap, PieceIndex.Rook, flags));
                    moves.Add(MoveEncoder.Encode(from, toCap, PieceIndex.Bishop, flags));
                    moves.Add(MoveEncoder.Encode(from, toCap, PieceIndex.Knight, flags));
                }
                else
                {
                    moves.Add(MoveEncoder.Encode(from, toCap, 0, flags));
                }
            }
        }

        moves.AddRange(
            SlidingMoves
                .GenerateBishopMoves(board, ownBishops, occupancy, isWhite)
                .Select(move =>
                    {
                        MoveFlags flags = ((1UL << move.to) & enemyPieces) != 0 ? MoveFlags.Capture : 0;
                        return MoveEncoder.Encode(move.from, move.to, 0, flags);
                    }
                )
        );

        moves.AddRange(
            SlidingMoves
                .GenerateRookMoves(board, ownRooks, occupancy, isWhite)
                .Select(move =>
                    {
                        MoveFlags flags = ((1UL << move.to) & enemyPieces) != 0 ? MoveFlags.Capture : 0;
                        return MoveEncoder.Encode(move.from, move.to, 0, flags);
                    }
                )
        );

        moves.AddRange(
            SlidingMoves
                .GenerateBishopMoves(board, ownQueens, occupancy, isWhite)
                .Select(move =>
                    {
                        MoveFlags flags = ((1UL << move.to) & enemyPieces) != 0 ? MoveFlags.Capture : 0;
                        return MoveEncoder.Encode(move.from, move.to, 0, flags);
                    }
                )
        );

        moves.AddRange(
            SlidingMoves
                .GenerateRookMoves(board, ownQueens, occupancy, isWhite)
                .Select(move =>
                    {
                        MoveFlags flags = ((1UL << move.to) & enemyPieces) != 0 ? MoveFlags.Capture : 0;
                        return MoveEncoder.Encode(move.from, move.to, 0, flags);
                    }
                )
        );

        return moves;
    }

    private static ulong GenerateKnightAttacks(int sq)
    {
        ulong attacks = 0;
        int rank = sq / 8;
        int file = sq % 8;

        int[] dr = { -2, -1, 1, 2, 2, 1, -1, -2 };
        int[] df = { 1, 2, 2, 1, -1, -2, -2, -1 };

        for (int i = 0; i < 8; i++)
        {
            int r = rank + dr[i];
            int f = file + df[i];
            if (r >= 0 && r < 8 && f >= 0 && f < 8)
            {
                attacks |= 1UL << (r * 8 + f);
            }
        }

        return attacks;
    }

    private static ulong GenerateKingAttacks(int sq)
    {
        ulong attacks = 0;
        int rank = sq / 8;
        int file = sq % 8;

        for (int dr = -1; dr <= 1; dr++)
            for (int df = -1; df <= 1; df++)
            {
                if (dr == 0 && df == 0) continue;
                int r = rank + dr;
                int f = file + df;
                if (r >= 0 && r < 8 && f >= 0 && f < 8)
                {
                    attacks |= 1UL << (r * 8 + f);
                }
            }

        return attacks;
    }

    private static ulong GenerateWhitePawnAttacks(int sq)
    {
        ulong attacks = 0;
        int rank = sq / 8;
        int file = sq % 8;

        if (rank < 7)
        {
            if (file > 0) attacks |= 1UL << ((rank + 1) * 8 + file - 1);
            if (file < 7) attacks |= 1UL << ((rank + 1) * 8 + file + 1);
        }

        return attacks;
    }

    private static ulong GenerateBlackPawnAttacks(int sq)
    {
        ulong attacks = 0;
        int rank = sq / 8;
        int file = sq % 8;

        if (rank > 0)
        {
            if (file > 0) attacks |= 1UL << ((rank - 1) * 8 + file - 1);
            if (file < 7) attacks |= 1UL << ((rank - 1) * 8 + file + 1);
        }

        return attacks;
    }
}
