using Knightmare.Boards;
using Knightmare.Moves;
using Knightmare.Pieces;

internal class BoardParser
{
    public BoardParser() { }

    public static Board Create(string _fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR/ w - - 0 0")
    {
        Dictionary<Point, Piece> white = new();
        Dictionary<Point, Piece> black = new();
        Board board = new();

        string[] fenParts = _fen.Split(' ');
        string piecePlacement = fenParts[0];
        char sideToMove = fenParts[1][0];

        string[] ranks = piecePlacement.Split('/');

        for (int y = 0; y < 8; y++)
        {
            int x = 0;
            foreach (char k in ranks[y])
            {
                if (char.IsDigit(k))
                {
                    for (int i = 0; i < (int)char.GetNumericValue(k); x++, i++)
                    {
                        Point point = new Point(x, y);
                        Tile tile = new Tile(point);
                        board.Tiles[y, x] = tile;
                    }
                }
                else
                {
                    Point point = new Point(x, y);
                    PlayerSide side = char.IsUpper(k) ? PlayerSide.White : PlayerSide.Black;
                    Piece piece = Piece.Create(k, point, side);

                    Tile tile = new Tile(point, piece);
                    board.Tiles[y, x] = tile;

                    if (side == PlayerSide.White)
                        white[point] = piece;
                    else
                        black[point] = piece;

                    x++;
                }
            }
        }

        board.SideToMove = (sideToMove == 'w' ? PlayerSide.White : PlayerSide.Black);
        board.WhitePieces = white;
        board.BlackPieces = black;

        return board;
    }

    public static Board CreateUCI(string _inputUCI)
    {
        if (string.IsNullOrWhiteSpace(_inputUCI))
            throw new ArgumentException("UCI input cannot be null or empty");

        string[] commands = _inputUCI.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (commands.Length < 2 || commands[0] != "position")
            throw new ArgumentException("Invalid UCI command");

        Board board;
        string[] moves = Array.Empty<string>();

        if (commands[1] == "startpos")
        {
            board = Create();
            if (commands.Length > 2 && commands[2] == "moves")
            {
                moves = commands.Skip(3).ToArray();
            }
        }
        else if (commands[1] == "fen")
        {
            if (commands.Length < 8)
                throw new ArgumentException("Incomplete FEN string");

            string fen = string.Join(" ", commands.Skip(2).Take(6));
            board = Create(fen);

            int moveIndex = 8;
            if (commands.Length > moveIndex && commands[moveIndex] == "moves")
            {
                moves = commands.Skip(moveIndex + 1).ToArray();
            }
        }
        else
        {
            throw new ArgumentException("Unsupported position type");
        }

        foreach (string move in moves)
        {
            Move parsedMove = Move.Create(move, board);
            parsedMove.Execute(board);
        }

        board.SideToMove = (moves.Length % 2 == 0) ? PlayerSide.White : PlayerSide.Black;

        return board;
    }

    public static string FEN(Board board)
    {
        Tile[,] tiles = board.Tiles;
        string fen = "";

        for (int i = 0; i < 8; i++)
        {
            int empty = 0;
            for (int j = 0; j < 8; ++j)
            {
                Piece piece = tiles[i, j].TilePiece!;
                if (piece == null)
                {
                    empty++;
                }
                else
                {
                    if (empty > 0)
                    {
                        fen += empty.ToString();
                        empty = 0;
                    }
                    fen += piece.Notation;
                }
            }

            if (empty > 0)
            {
                fen += empty.ToString();
            }

            if (i < 7) fen += '/';
        }

        fen += " ";
        fen += (board.SideToMove == PlayerSide.White) ? "w" : "b";

        //string castling = GetCastlingRights();
        //fen += " " + (castling == "" ? "-" : castling);

        //string enPassant = GetEnPassantSquare();
        //fen += " " + (enPassant == "" ? "-" : enPassant);

        fen += " - - 0 0";

        return fen;
    }
}
