using Knightmare.Pieces;

namespace Knightmare.Boards
{
    internal class Board
    {
        public PlayerSide SideToMove { get; set; }
        public bool Terminal { get; set; }
        public Tile[,] Tiles { get; }

        public Dictionary<Point, Piece> WhitePieces;
        public Dictionary<Point, Piece> BlackPieces;

        public Board()
            : this(new Dictionary<Point, Piece>(),
                   new Dictionary<Point, Piece>(),
                   new Tile[8, 8])
        { }

        public Board(Dictionary<Point, Piece> _whitePieces,
                     Dictionary<Point, Piece> _blackPieces,
                     Tile[,] _tiles)
        {
            Tiles = _tiles;
            WhitePieces = _whitePieces;
            BlackPieces = _blackPieces;
            SideToMove = PlayerSide.White;
        }

        public Tile Tile(int x, int y)
        {
            return Tiles[y, x];
        }

        public Tile Tile(Point point)
        {
            return Tiles[point.y, point.x];
        }

        public List<Piece> SidePieces()
        {
            return (SideToMove == PlayerSide.White)
                ? WhitePieces.Values.ToList()
                : BlackPieces.Values.ToList();
        }


        public ulong Hash()
        {
            ulong hash = 0;

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Piece? p = Tiles[y, x].TilePiece;
                    if (p != null)
                    {
                        int index = p.PieceIndex();
                        int side = p.Side == PlayerSide.White ? 0 : 1;
                        int squareIndex = y * 8 + x;
                        hash ^= Zobrist.PieceSquare[index, side, squareIndex];
                    }
                }
            }

            if (SideToMove == PlayerSide.White)
            {
                hash ^= Zobrist.SideToMove;
            }

            return hash;
        }
    }
}
