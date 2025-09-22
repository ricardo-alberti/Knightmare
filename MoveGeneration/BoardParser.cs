internal class BoardParser
{
    private const string StartPosFEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
    //private const string StartPosFEN = "rnbqkb1r/1ppppppp/p6B/8/2BPP3/5Q2/PPP2PPP/RN2K1NR b - - 0 0";
    //private const string StartPosFEN = "r1bqkb1r/pppppppp/6n1/3P4/4PP2/2N5/PPPQ1nPP/R3KBNR w KQkq - 0 9";

    public char GetPieceCharAtSquare(Board board, int square)
    {
        ulong mask = 1UL << square;

        if ((board.WhiteKnights & mask) != 0) return 'N';
        if ((board.WhitePawns & mask) != 0) return 'P';
        if ((board.WhiteBishops & mask) != 0) return 'B';
        if ((board.WhiteRooks & mask) != 0) return 'R';
        if ((board.WhiteQueens & mask) != 0) return 'Q';
        if ((board.WhiteKing & mask) != 0) return 'K';

        if ((board.BlackKnights & mask) != 0) return 'n';
        if ((board.BlackPawns & mask) != 0) return 'p';
        if ((board.BlackBishops & mask) != 0) return 'b';
        if ((board.BlackRooks & mask) != 0) return 'r';
        if ((board.BlackQueens & mask) != 0) return 'q';
        if ((board.BlackKing & mask) != 0) return 'k';

        return '_';
    }

    public string CreateFENFromBoard(Board board) 
    {
        string ret = "";

        for (int r = 7; r >= 0; --r)
        {
            int emptyCount = 0;

            for (int f = 0; f < 8; ++f)
            {
                char piece = GetPieceCharAtSquare(board, f + (8 * r));

                if (piece == '_')
                {
                    emptyCount++;
                }
                else
                {
                    if (emptyCount > 0)
                    {
                        ret += emptyCount.ToString();
                        emptyCount = 0;
                    }

                    ret += piece;
                }
            }

            if (emptyCount > 0)
            {
                ret += emptyCount.ToString();
            }

            if (r > 0)
            {
                ret += '/';
            }
        }

        ret += ' ';
        ret += board.WhiteToMove ? 'w' : 'b';
        ret += " - - 0 0";

        return ret;
    }

    public Board CreateBoardFromUCI(string uciInput)
    {
        string[] parts = uciInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        Board board;

        int movesIndex = Array.IndexOf(parts, "moves");

        if (parts.Length > 1 && parts[1] == "startpos")
        {
            board = CreateBoardFromFEN(StartPosFEN);
        }
        else if (parts.Length > 1 && parts[1] == "fen")
        {
            int fenStart = 2;
            int fenEnd = movesIndex > 0 ? movesIndex : parts.Length;
            string fen = string.Join(' ', parts[fenStart..fenEnd]);
            board = CreateBoardFromFEN(fen);
        }
        else
        {
            throw new ArgumentException("UCI input must specify 'startpos' or 'fen'");
        }

        if (movesIndex >= 0)
        {
            for (int i = movesIndex + 1; i < parts.Length; i++)
            {
                string moveStr = parts[i];
                int move = MoveEncoder.Encode(moveStr, board);
                board.MakeMove(move);
            }
        }

        return board;
    }

    public Board CreateBoardFromFEN(string fen = StartPosFEN)
    {
        var board = new Board();

        board.WhitePawns = 0UL;
        board.WhiteRooks = 0UL;
        board.WhiteKnights = 0UL;
        board.WhiteBishops = 0UL;
        board.WhiteQueens = 0UL;
        board.WhiteKing = 0UL;

        board.BlackPawns = 0UL;
        board.BlackRooks = 0UL;
        board.BlackKnights = 0UL;
        board.BlackBishops = 0UL;
        board.BlackQueens = 0UL;
        board.BlackKing = 0UL;

        string[] parts = fen.Split(' ');
        if (parts.Length < 4)
            throw new ArgumentException("Invalid FEN string: missing fields");

        string[] ranks = parts[0].Split('/');
        if (ranks.Length != 8)
            throw new ArgumentException("Invalid FEN string ranks");

        for (int rank = 0; rank < 8; rank++)
        {
            string rankStr = ranks[rank];
            int file = 0;

            foreach (char c in rankStr)
            {
                if (char.IsDigit(c))
                {
                    file += c - '0';
                }
                else
                {
                    int squareIndex = (7 - rank) * 8 + file;
                    ulong bit = 1UL << squareIndex;

                    switch (c)
                    {
                        case 'P': board.WhitePawns |= bit; break;
                        case 'R': board.WhiteRooks |= bit; break;
                        case 'N': board.WhiteKnights |= bit; break;
                        case 'B': board.WhiteBishops |= bit; break;
                        case 'Q': board.WhiteQueens |= bit; break;
                        case 'K': board.WhiteKing |= bit; break;

                        case 'p': board.BlackPawns |= bit; break;
                        case 'r': board.BlackRooks |= bit; break;
                        case 'n': board.BlackKnights |= bit; break;
                        case 'b': board.BlackBishops |= bit; break;
                        case 'q': board.BlackQueens |= bit; break;
                        case 'k': board.BlackKing |= bit; break;

                        default:
                            throw new ArgumentException($"Invalid piece character '{c}' in FEN");
                    }

                    file++;
                }
            }
        }

        board.WhiteToMove = parts[1] == "w";

        string castling = parts[2];
        board.WhiteCanCastleKingSide = castling.Contains('K');
        board.WhiteCanCastleQueenSide = castling.Contains('Q');
        board.BlackCanCastleKingSide = castling.Contains('k');
        board.BlackCanCastleQueenSide = castling.Contains('q');

        board.HalfmoveClock = parts.Length > 4 ? int.Parse(parts[4]) : 0;
        board.FullmoveNumber = parts.Length > 5 ? int.Parse(parts[5]) : 1;

        return board;
    }
}

