using Knightmare.Moves;
using Knightmare.Pieces;

namespace Knightmare.Boards
{
    internal class Board
    {
        private readonly PieceCollection whitePieces;
        private readonly PieceCollection blackPieces;
        private readonly Tile[,] tiles;
        private readonly Stack<Move> history;

        public Board()
            : this(new PieceCollection(), new PieceCollection(), new Tile[8, 8])
        {

        }

        public Board(PieceCollection _whitePieces, PieceCollection _blackPieces)
            : this(_whitePieces, _blackPieces, new Tile[8, 8])
        {

        }

        public Board(PieceCollection _whitePieces, PieceCollection _blackPieces, Tile[,] _tiles)
        {
            whitePieces = _whitePieces;
            blackPieces = _blackPieces;
            tiles = _tiles;
            history = new Stack<Move>();
        }


        public Tile[,] Tiles()
        {
            return tiles;
        }

        public Board Update(Move _move)
        {
            history.Push(_move);
            Tile[] newTiles = _move.Tiles();
            Tile[,] updatedTiles = tiles;

            ChessPiece pieceUpdated = newTiles[1].Piece();

            foreach (var tile in newTiles)
            {
                int x = tile.Position().x;
                int y = tile.Position().y;
                updatedTiles[y, x] = tile;
            }

            var whitePiecesUpdated = whitePieces.List();
            var blackPiecesUpdated = blackPieces.List();

            var start = newTiles[0].Position();
            var end = newTiles[1].Position();

            if (pieceUpdated.Side() == PlayerSide.White)
            {
                whitePiecesUpdated.Remove(start);
                whitePiecesUpdated[end] = pieceUpdated;
                blackPiecesUpdated.Remove(end);
            }
            else
            {
                blackPiecesUpdated.Remove(start);
                blackPiecesUpdated[end] = pieceUpdated;
                whitePiecesUpdated.Remove(end);
            }

            return new Board(new PieceCollection(whitePiecesUpdated), new PieceCollection(blackPiecesUpdated), updatedTiles);
        }

        public void Undo()
        {
            Move lastMove = history.Pop();
            
            this.Update(lastMove.Undo());
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
            Dictionary<Point, ChessPiece> pieces = whitePieces.List();

            if (_side == PlayerSide.Black)
            {
                pieces = blackPieces.List();
            }

            return pieces;
        }

        public Dictionary<Point, ChessPiece> WhitePieces()
        {
            return whitePieces.List();
        }

        public Dictionary<Point, ChessPiece> BlackPieces()
        {
            return blackPieces.List();
        }

        static public Board Create(string _fen = "RNBQKBNR/PPPPPPPP/8/8/8/8/pppppppp/rnbqkbnr")
        {
            Board board = new();
            Tile tile = new();
            Point point = new();
            PieceCollection white = new();
            PieceCollection black = new();
            int x = 0;
            int y = 0;

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
                    point = new Point(x, y);

                    if (Char.IsUpper(k))
                    {
                        tile = new Tile(ChessPiece.Create(k, point, PlayerSide.White), point);
                        white.Add(tile.Piece());
                    }
                    else if (char.IsLower(k))
                    {
                        tile = new Tile(ChessPiece.Create(k, point, PlayerSide.Black), point);
                        black.Add(tile.Piece());
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
