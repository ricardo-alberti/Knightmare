using Knightmare.Moves;
using Knightmare.Pieces;

namespace Knightmare.Boards
{
    internal class Board
    {
        private readonly Tile[,] tiles;
        private readonly Stack<Move> history;
        private readonly Dictionary<Point, ChessPiece> whitePieces;
        private readonly Dictionary<Point, ChessPiece> blackPieces;

        public Board()
            : this(new Dictionary<Point, ChessPiece>(),
                new Dictionary<Point, ChessPiece>(), new Tile[8, 8])
        {

        }

        public Board(Dictionary<Point, ChessPiece> _whitePieces,
                Dictionary<Point, ChessPiece> _blackPieces)
            : this(_whitePieces, _blackPieces, new Tile[8, 8])
        {

        }

        public Board(Dictionary<Point, ChessPiece> _whitePieces,
                Dictionary<Point, ChessPiece> _blackPieces, Tile[,] _tiles)
            : this(_whitePieces, _blackPieces, _tiles, new Stack<Move>())
        {

        }

        public Board(Dictionary<Point, ChessPiece> _whitePieces,
                Dictionary<Point, ChessPiece> _blackPieces,
                Tile[,] _tiles, Stack<Move> _history)
        {
            whitePieces = _whitePieces;
            blackPieces = _blackPieces;
            tiles = _tiles;
            history = _history;
        }


        public Stack<Move> History()
        {
            return history;
        }

        public Tile[,] Tiles()
        {
            return tiles;
        }

        public void Update(Move _move, bool _keepHistory = true)
        {
            if (_keepHistory)
                history.Push(_move);

            Tile[] newTiles = _move.Tiles();
            ChessPiece pieceUpdated = newTiles[1].Piece();

            foreach (var tile in newTiles)
            {
                int x = tile.Position().x;
                int y = tile.Position().y;
                tiles[y, x] = tile;
            }

            var start = newTiles[0].Position();
            var end = newTiles[1].Position();

            if (pieceUpdated.Side() == PlayerSide.White)
            {
                whitePieces.Remove(start);
                whitePieces[end] = pieceUpdated;
                blackPieces.Remove(end);
            }
            else
            {
                blackPieces.Remove(start);
                blackPieces[end] = pieceUpdated;
                whitePieces.Remove(end);
            }
        }

        public void Undo()
        {
            if (history.Count == 0)
                return;

            Move lastMove = history.Pop();

            this.Update(lastMove.Undo(), false);
        }

        public Board Copy()
        {
            return Create(FEN());
        }

        public Tile Tile(int x, int y)
        {
            return tiles[y, x];
        }

        public Dictionary<Point, ChessPiece> SidePieces(PlayerSide _side)
        {
            Dictionary<Point, ChessPiece> pieces = whitePieces;

            if (_side == PlayerSide.Black)
            {
                pieces = blackPieces;
            }

            return pieces;
        }

        public Dictionary<Point, ChessPiece> WhitePieces()
        {
            return whitePieces;
        }

        public Dictionary<Point, ChessPiece> BlackPieces()
        {
            return blackPieces;
        }

        static public Board Create(string _fen = "RNBQKBNR/PPPPPPPP/8/8/8/8/pppppppp/rnbqkbnr")
        {
            int x = 0;
            int y = 0;

            Board board = new();
            Tile tile = new();
            Point point = new();
            Dictionary<Point, ChessPiece> white = new();
            Dictionary<Point, ChessPiece> black = new();

            foreach (char k in _fen)
            {
                if (k == '/')
                {
                    x = 0;
                    y++;
                }
                else if (char.IsDigit(k))
                {
                    int num = (int)char.GetNumericValue(k);

                    for (int i = 0; i < num; i++)
                    {
                        point = new Point(x, y);
                        board.Tiles()[y, x] = new Tile(point);
                        x++;
                    }
                }
                else
                {
                    ChessPiece piece;
                    point = new Point(x, y);

                    if (Char.IsUpper(k))
                    {
                        tile = new Tile(ChessPiece.Create(k, point, PlayerSide.White), point);
                        piece = tile.Piece();
                        white[piece.Position()] = piece;
                    }
                    else if (char.IsLower(k))
                    {
                        tile = new Tile(ChessPiece.Create(k, point, PlayerSide.Black), point);
                        piece = tile.Piece();
                        black[piece.Position()] = piece;
                    }

                    board.Tiles()[y, x] = tile;
                    x++;
                }
            }

            return new Board(white, black, board.Tiles());
        }

        public string FEN()
        {
            Tile[,] tiles = this.Tiles();
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

            return fen;
        }
    }
}
