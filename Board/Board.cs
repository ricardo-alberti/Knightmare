internal class Board
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

    public void MakeMove(int move)
    {
        int from = MoveEncoder.FromSquare(move);
        int to = MoveEncoder.ToSquare(move);
        int prom = MoveEncoder.Promotion(move);
        MoveFlags flags = MoveEncoder.Flags(move);

        int piece = GetPieceAtSquare(from, WhiteToMove);

        if (piece == PieceIndex.Null)
        {
            throw new Exception("Invalid move");
        }

        RemovePiece(piece, from, WhiteToMove);

        if ((flags & MoveFlags.Capture) != 0)
            CapturePieceAt(to, !WhiteToMove);

        if ((flags & MoveFlags.Promotion) != 0)
        {
            int capSq = WhiteToMove ? to - 8 : to + 8;
            RemovePiece(PieceIndex.Pawn, capSq, !WhiteToMove);
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
                if (to == 6) { 
                    RemovePiece(PieceIndex.Rook, 7, WhiteToMove); 
                    AddPiece(PieceIndex.Rook, 5, WhiteToMove); }
                else if (to == 2) { 
                    RemovePiece(PieceIndex.Rook, 0, WhiteToMove); 
                    AddPiece(PieceIndex.Rook, 3, WhiteToMove); 
                }
            }
        }

        if (prom != 0)
        {
            AddPiece(prom, to, WhiteToMove);
        }
        else
        {
            AddPiece(piece, to, WhiteToMove);
        }

        WhiteToMove = !WhiteToMove;
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

    public void CapturePieceAt(int square, bool white)
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

    void RemovePiece(int piece, int square, bool white)
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

    void AddPiece(int piece, int square, bool white)
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

    public ulong? EnPassantSquare
    {
        get
        {
            ulong val = Metadata & 0b111111;
            return val == 63 ? null : val;
        }
        set
        {
            Metadata &= ~0b111111UL;
            Metadata |= value ?? 63;
        }
    }

    public bool WhiteCanCastleKingSide
    {
        get => (Metadata & (1UL << 6)) != 0;
        set
        {
            if (value) Metadata |= (1UL << 6);
            else Metadata &= ~(1UL << 6);
        }
    }

    public bool WhiteCanCastleQueenSide
    {
        get => (Metadata & (1UL << 7)) != 0;
        set
        {
            if (value) Metadata |= (1UL << 7);
            else Metadata &= ~(1UL << 7);
        }
    }

    public bool BlackCanCastleKingSide
    {
        get => (Metadata & (1UL << 8)) != 0;
        set
        {
            if (value) Metadata |= (1UL << 8);
            else Metadata &= ~(1UL << 8);
        }
    }

    public bool BlackCanCastleQueenSide
    {
        get => (Metadata & (1UL << 9)) != 0;
        set
        {
            if (value) Metadata |= (1UL << 9);
            else Metadata &= ~(1UL << 9);
        }
    }

    public int HalfmoveClock
    {
        get => (int)((Metadata >> 10) & 0xFF);
        set
        {
            Metadata &= ~(0xFFUL << 10);
            Metadata |= ((ulong)value & 0xFFUL) << 10;
        }
    }

    public int FullmoveNumber
    {
        get => (int)((Metadata >> 18) & 0x3FF);
        set
        {
            Metadata &= ~(0x3FFUL << 18);
            Metadata |= ((ulong)value & 0x3FFUL) << 18;
        }
    }

    public bool WhiteToMove
    {
        get => (Metadata & (1UL << 28)) != 0;
        set
        {
            if (value) Metadata |= (1UL << 28);
            else Metadata &= ~(1UL << 28);
        }
    }
}
