using Knightmare.Moves;
using Knightmare.Pieces;

namespace Knightmare.Boards
{
    internal class Board
    {
        public Dictionary<Point, ChessPiece> WhitePieces;
        public Dictionary<Point, ChessPiece> BlackPieces;
        public PlayerSide SidePlayable { get; set; } = PlayerSide.White;
        private Tile[,] Tiles { get; set; }

        public Board()
            : this(new Dictionary<Point, ChessPiece>(),
                   new Dictionary<Point, ChessPiece>(),
                   new Tile[8, 8])
        {

        }

        public Board(Dictionary<Point, ChessPiece> _whitePieces,
                     Dictionary<Point, ChessPiece> _blackPieces,
                     Tile[,] _tiles)
        {
            Tiles = _tiles;
            WhitePieces = _whitePieces;
            BlackPieces = _blackPieces;
        }

        public static Board CreateUCI(string _inputUCI)
        {
            //position startpos e2e4
            if (string.IsNullOrWhiteSpace(_inputUCI))
                throw new ArgumentException("UCI input cannot be null or empty");

            string[] commands = _inputUCI.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (commands.Length < 2 || commands[0] != "position")
                throw new ArgumentException("Invalid UCI command");

            Board board;
            string[] moves = Array.Empty<string>();

            if (commands[1] == "startpos")
            {
                board = Board.Create();
                if (commands.Length > 2 && commands[2] == "moves")
                {
                    moves = commands.Skip(3).ToArray();
                }
            }
            else if (commands[1] == "fen")
            {
                if (commands.Length < 3)
                    throw new ArgumentException("FEN string is missing");

                board = Board.Create(commands[2]);
                if (commands.Length > 3 && commands[3] == "moves")
                {
                    moves = commands.Skip(4).ToArray();
                }
            }
            else
            {
                throw new ArgumentException("Unsupported position type");
            }

            foreach (string move in moves)
            {
                Move? parsedMove = Move.Create(move, board);
                if (parsedMove == null)
                    throw new ArgumentException($"Invalid move: {move}");

                board.Update(parsedMove);
            }

            board.SidePlayable = (moves.Length % 2 == 0) ? PlayerSide.White : PlayerSide.Black;

            return board;
        }

        public void Update(Move _move)
        {
            Tile[] newTiles = _move.Tiles();
            ChessPiece pieceUpdated = newTiles[1].Piece();

            foreach (var tile in newTiles)
            {
                int x = tile.Position().x;
                int y = tile.Position().y;
                Tiles[y, x] = tile;
            }

            var start = newTiles[0].Position();
            var end = newTiles[1].Position();

            if (pieceUpdated.Side() == PlayerSide.White)
            {
                SidePlayable = PlayerSide.Black;
                WhitePieces.Remove(start);
                WhitePieces[end] = pieceUpdated;
                BlackPieces.Remove(end);
            }
            else
            {
                SidePlayable = PlayerSide.White;
                BlackPieces.Remove(start);
                BlackPieces[end] = pieceUpdated;
                WhitePieces.Remove(end);
            }
        }

        public Board Copy()
        {
            return Create(FEN());
        }

        public Tile Tile(int x, int y)
        {
            return Tiles[y, x];
        }

        public Dictionary<Point, ChessPiece> SidePieces()
        {
            Dictionary<Point, ChessPiece> pieces = WhitePieces;

            if (SidePlayable == PlayerSide.Black)
            {
                pieces = BlackPieces;
            }

            return pieces;
        }

        static public Board Create(string _fen = "RNBKQBNR/PPPPPPPP/8/8/8/8/pppppppp/rnbkqbnr/ w - - 0 0")
        {
            Board board = new();
            Dictionary<Point, ChessPiece> white = new();
            Dictionary<Point, ChessPiece> black = new();

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
                            PlayerSide side = char.IsUpper(k) ? PlayerSide.White : PlayerSide.Black;
                            ChessPiece piece = new Piece(); //placeholder
                            Tile tile = new Tile(piece, point);
                            board.Tiles[y, x] = tile;
                        }
                    }
                    else
                    {
                        Point point = new Point(x, y);
                        PlayerSide side = char.IsUpper(k) ? PlayerSide.White : PlayerSide.Black;
                        ChessPiece piece = ChessPiece.Create(k, point, side);

                        Tile tile = new Tile(piece, point);
                        board.Tiles[y, x] = tile;

                        if (side == PlayerSide.White)
                            white[point] = piece;
                        else
                            black[point] = piece;

                        x++;
                    }
                }
            }

            board.SidePlayable = (sideToMove == 'w' ? PlayerSide.White : PlayerSide.Black);
            board.WhitePieces = white;
            board.BlackPieces = black;

            return board;
        }

        public string FEN()
        {
            Tile[,] tiles = this.Tiles;
            string fen = "";

            for (int i = 0; i < 8; i++)
            {
                int empty = 0;
                for (int j = 0; j < 8; ++j)
                {
                    if (tiles[i, j].Piece().Side() != PlayerSide.None)
                    {
                        if (empty != 0)
                        {
                            fen += empty.ToString();
                        }

                        fen += tiles[i, j].Piece().Notation();
                        empty = 0;
                        continue;
                    }

                    empty++;
                }

                if (empty != 0)
                {
                    fen += empty.ToString();
                    fen += '/';
                    continue;
                }

                fen += '/';
            }

            fen = fen.Remove(fen.Length - 1);

            fen += " ";
            if (SidePlayable == PlayerSide.White)
            {
                fen += "w";
            }
            else
            {
                fen += "b";
            }

            fen += " - - 0 0";

            return fen;
        }
    }
}
