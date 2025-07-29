internal partial class Board
{
    public ulong WhitePawns { get; set; }
    public ulong WhiteRooks { get; set; }
    public ulong WhiteKnights { get; set; }
    public ulong WhiteBishops { get; set; }
    public ulong WhiteQueens { get; set; }
    public ulong WhiteKing { get; set; }
    public ulong BlackPawns { get; set; }
    public ulong BlackRooks { get; set; }
    public ulong BlackKnights { get; set; }
    public ulong BlackBishops { get; set; }
    public ulong BlackQueens { get; set; }
    public ulong BlackKing { get; set; }

    public ulong Metadata { get; set; }

    public ulong Occupied => WhitePieces | BlackPieces;
    public ulong Empty => ~Occupied;

    public ulong WhitePieces => WhitePawns | WhiteRooks | WhiteKnights | WhiteBishops | WhiteQueens | WhiteKing;
    public ulong BlackPieces => BlackPawns | BlackRooks | BlackKnights | BlackBishops | BlackQueens | BlackKing;

    public bool IsInCheck(bool isWhite)
    {
        ulong kingBB = isWhite ? WhiteKing : BlackKing;
        if (kingBB == 0)
            return false; 

        int kingSq = Bitboard.GetFirstSetBit(kingBB);

        ulong enemyPawns = isWhite ? BlackPawns : WhitePawns;
        ulong enemyKnights = isWhite ? BlackKnights : WhiteKnights;
        ulong enemyBishops = isWhite ? BlackBishops : WhiteBishops;
        ulong enemyRooks = isWhite ? BlackRooks : WhiteRooks;
        ulong enemyQueens = isWhite ? BlackQueens : WhiteQueens;
        ulong enemyKing = isWhite ? BlackKing : WhiteKing;

        ulong allPieces = WhitePieces | BlackPieces;

        ulong pawnAttacks = isWhite
            ? MoveGenerator.BlackPawnAttacks[kingSq]
            : MoveGenerator.WhitePawnAttacks[kingSq];

        if ((pawnAttacks & enemyPawns) != 0)
            return true;

        if ((MoveGenerator.KnightAttacks[kingSq] & enemyKnights) != 0)
            return true;

        if ((SlidingMoves.GetBishopAttacks(kingSq, allPieces) & (enemyBishops | enemyQueens)) != 0)
            return true;

        if ((SlidingMoves.GetRookAttacks(kingSq, allPieces) & (enemyRooks | enemyQueens)) != 0)
            return true;

        if ((MoveGenerator.KingAttacks[kingSq] & enemyKing) != 0)
            return true;

        return false;
    }

    public MoveState MakeMove(int move)
    {
        int from = MoveEncoder.FromSquare(move);
        int to = MoveEncoder.ToSquare(move);
        int prom = MoveEncoder.Promotion(move);
        MoveFlags flags = MoveEncoder.Flags(move);

        int piece = GetPieceAtSquare(from, WhiteToMove);

        if (piece == PieceIndex.Null) throw new Exception("Invalid move");

        MoveState state = new MoveState
        {
            Metadata = this.Metadata,
            CapturedPiece = PieceIndex.Null,
            CapturedSquare = to,
            PieceMoved = piece,
        };

        if ((flags & MoveFlags.Capture) != 0)
        {
            if ((flags & MoveFlags.Promotion) != 0)
            {
                int epCapSq = WhiteToMove ? to - 8 : to + 8;
                state.CapturedPiece = GetPieceAtSquare(epCapSq, !WhiteToMove);
                state.CapturedSquare = epCapSq;
                RemovePiece(PieceIndex.Pawn, epCapSq, !WhiteToMove);
            }
            else
            {
                state.CapturedPiece = GetPieceAtSquare(to, !WhiteToMove);
                CapturePieceAt(to, !WhiteToMove);
            }
        }

        if ((flags & MoveFlags.KingCastle) != 0 || (flags & MoveFlags.QueenCastle) != 0)
        {
            if (WhiteToMove)
            {
                if (to == 62)
                {
                    RemovePiece(PieceIndex.Rook, 63, WhiteToMove);
                    AddPiece(PieceIndex.Rook, 61, WhiteToMove);
                }
                else if (to == 58)
                {
                    RemovePiece(PieceIndex.Rook, 56, WhiteToMove);
                    AddPiece(PieceIndex.Rook, 59, WhiteToMove);
                }
            }
            else
            {
                if (to == 6)
                {
                    RemovePiece(PieceIndex.Rook, 7, WhiteToMove);
                    AddPiece(PieceIndex.Rook, 5, WhiteToMove);
                }
                else if (to == 2)
                {
                    RemovePiece(PieceIndex.Rook, 0, WhiteToMove);
                    AddPiece(PieceIndex.Rook, 3, WhiteToMove);
                }
            }
        }

        RemovePiece(piece, from, WhiteToMove);

        if (prom != PieceIndex.Null) 
        {
            AddPiece(prom, to, WhiteToMove);
        }
        else
        {
            AddPiece(piece, to, WhiteToMove);
        }

        WhiteToMove = !WhiteToMove;

        return state;
    }

    public void UndoMove(MoveState state, int move)
    {
        int from = MoveEncoder.FromSquare(move);
        int to = MoveEncoder.ToSquare(move);
        int prom = MoveEncoder.Promotion(move);
        MoveFlags flags = MoveEncoder.Flags(move);

        WhiteToMove = !WhiteToMove;

        if (prom != PieceIndex.Null)
        {
            RemovePiece(prom, to, WhiteToMove);
            AddPiece(PieceIndex.Pawn, from, WhiteToMove);
        }
        else
        {
            RemovePiece(state.PieceMoved, to, WhiteToMove);
            AddPiece(state.PieceMoved, from, WhiteToMove);
        }

        if ((flags & MoveFlags.KingCastle) != 0 || (flags & MoveFlags.QueenCastle) != 0)
        {
            if (WhiteToMove)
            {
                if (to == 62)
                {
                    RemovePiece(PieceIndex.Rook, 61, WhiteToMove);
                    AddPiece(PieceIndex.Rook, 63, WhiteToMove);
                }
                else if (to == 58)
                {
                    RemovePiece(PieceIndex.Rook, 59, WhiteToMove);
                    AddPiece(PieceIndex.Rook, 56, WhiteToMove);
                }
            }
            else
            {
                if (to == 6)
                {
                    RemovePiece(PieceIndex.Rook, 5, WhiteToMove);
                    AddPiece(PieceIndex.Rook, 7, WhiteToMove);
                }
                else if (to == 2)
                {
                    RemovePiece(PieceIndex.Rook, 3, WhiteToMove);
                    AddPiece(PieceIndex.Rook, 0, WhiteToMove);
                }
            }
        }

        if (state.CapturedPiece != PieceIndex.Null)
        {
            AddPiece(state.CapturedPiece, state.CapturedSquare, !WhiteToMove);
        }

        Metadata = state.Metadata;
    }

    public int GetPieceAtSquare(int square, bool white)
    {
        ulong mask = 1UL << square;

        if (white)
        {
            if ((WhitePawns & mask) != 0) return PieceIndex.Pawn;
            else if ((WhiteKnights & mask) != 0) return PieceIndex.Knight;
            else if ((WhiteBishops & mask) != 0) return PieceIndex.Bishop;
            else if ((WhiteRooks & mask) != 0) return PieceIndex.Rook;
            else if ((WhiteQueens & mask) != 0) return PieceIndex.Queen;
            else if ((WhiteKing & mask) != 0) return PieceIndex.King;
        }
        else
        {
            if ((BlackPawns & mask) != 0) return PieceIndex.Pawn;
            else if ((BlackKnights & mask) != 0) return PieceIndex.Knight;
            else if ((BlackBishops & mask) != 0) return PieceIndex.Bishop;
            else if ((BlackRooks & mask) != 0) return PieceIndex.Rook;
            else if ((BlackQueens & mask) != 0) return PieceIndex.Queen;
            else if ((BlackKing & mask) != 0) return PieceIndex.King;
        }

        return PieceIndex.Null;
    }

    private void CapturePieceAt(int square, bool white)
    {
        ulong mask = 1UL << square;

        if (white)
        {
            if ((WhitePawns & mask) != 0) WhitePawns &= ~mask;
            else if ((WhiteKnights & mask) != 0) WhiteKnights &= ~mask;
            else if ((WhiteBishops & mask) != 0) WhiteBishops &= ~mask;
            else if ((WhiteRooks & mask) != 0) WhiteRooks &= ~mask;
            else if ((WhiteQueens & mask) != 0) WhiteQueens &= ~mask;
            else if ((WhiteKing & mask) != 0) WhiteKing &= ~mask;
        }
        else
        {
            if ((BlackPawns & mask) != 0) BlackPawns &= ~mask;
            else if ((BlackKnights & mask) != 0) BlackKnights &= ~mask;
            else if ((BlackBishops & mask) != 0) BlackBishops &= ~mask;
            else if ((BlackRooks & mask) != 0) BlackRooks &= ~mask;
            else if ((BlackQueens & mask) != 0) BlackQueens &= ~mask;
            else if ((BlackKing & mask) != 0) BlackKing &= ~mask;
        }
    }

    private void RemovePiece(int piece, int square, bool white)
    {
        ulong mask = ~(1UL << square);
        if (white)
        {
            switch (piece)
            {
                case PieceIndex.Pawn: WhitePawns &= mask; break;
                case PieceIndex.Knight: WhiteKnights &= mask; break;
                case PieceIndex.Bishop: WhiteBishops &= mask; break;
                case PieceIndex.Rook: WhiteRooks &= mask; break;
                case PieceIndex.Queen: WhiteQueens &= mask; break;
                case PieceIndex.King: WhiteKing &= mask; break;
            }
        }
        else
        {
            switch (piece)
            {
                case PieceIndex.Pawn: BlackPawns &= mask; break;
                case PieceIndex.Knight: BlackKnights &= mask; break;
                case PieceIndex.Bishop: BlackBishops &= mask; break;
                case PieceIndex.Rook: BlackRooks &= mask; break;
                case PieceIndex.Queen: BlackQueens &= mask; break;
                case PieceIndex.King: BlackKing &= mask; break;
            }
        }
    }

    private void AddPiece(int piece, int square, bool white)
    {
        ulong mask = 1UL << square;
        if (white)
        {
            switch (piece)
            {
                case PieceIndex.Pawn: WhitePawns |= mask; break;
                case PieceIndex.Knight: WhiteKnights |= mask; break;
                case PieceIndex.Bishop: WhiteBishops |= mask; break;
                case PieceIndex.Rook: WhiteRooks |= mask; break;
                case PieceIndex.Queen: WhiteQueens |= mask; break;
                case PieceIndex.King: WhiteKing |= mask; break;
            }
        }
        else
        {
            switch (piece)
            {
                case PieceIndex.Pawn: BlackPawns |= mask; break;
                case PieceIndex.Knight: BlackKnights |= mask; break;
                case PieceIndex.Bishop: BlackBishops |= mask; break;
                case PieceIndex.Rook: BlackRooks |= mask; break;
                case PieceIndex.Queen: BlackQueens |= mask; break;
                case PieceIndex.King: BlackKing |= mask; break;
            }
        }
    }
}
