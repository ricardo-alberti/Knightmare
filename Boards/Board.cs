using Knightmare.Moves;
using Knightmare.Pieces;

namespace Knightmare.Boards
{
    internal class Board
    {
        private readonly PieceCollection whitePieces;
        private readonly PieceCollection blackPieces;
        private readonly Tile[,] tiles;

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
        }


        public Tile[,] Tiles()
        {
            return tiles;
        }

        public Board Update(Move _move)
        {
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

        public Board Copy()
        {
            Dictionary<Point, ChessPiece> whitepiecesCopy = new Dictionary<Point, ChessPiece>();
            Dictionary<Point, ChessPiece> blackpiecesCopy = new Dictionary<Point, ChessPiece>();
            Tile[,] tilesCopy = new Tile[8, 8];

            foreach (var pair in whitePieces.List())
            {
                whitepiecesCopy[pair.Key] = pair.Value;
            }

            foreach (var pair in blackPieces.List())
            {
                blackpiecesCopy[pair.Key] = pair.Value;
            }

            foreach (Tile tile in tiles)
            {
                tilesCopy[tile.Position().y, tile.Position().x] = tile;
            }

            Board board = new Board(new PieceCollection(whitepiecesCopy), new PieceCollection(blackpiecesCopy), tilesCopy);
            board.SetPieces(whitepiecesCopy, blackpiecesCopy);

            return board;
        }

        public Tile Tile(int x, int y)
        {
            return tiles[y, x];
        }

        public Tile Tile(Point axis)
        {
            return tiles[axis.y, axis.x];
        }

        public Dictionary<Point, ChessPiece> SidePieces(PlayerSide _side)
        {
            Dictionary<Point, ChessPiece> pieces = whitePieces.List();

            if (_side == PlayerSide.Black)
            {
                pieces =  blackPieces.List();
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

        public void SetPieces(Dictionary<Point, ChessPiece> _whitePieces, Dictionary<Point, ChessPiece> _blackPieces)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Point position = new Point(j, i);
                    Tile tile = new Tile(position);

                    foreach (var piece in _whitePieces.Values.Concat(_blackPieces.Values))
                    {
                        if (piece.Position().Equals(position))
                        {
                            tile = new Tile(piece, position);
                            break;
                        }
                    }

                    tiles[i, j] = tile;
                }
            }
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
