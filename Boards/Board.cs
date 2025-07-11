using Knightmare.Pieces;

namespace Knightmare.Boards
{
    internal class Board
    {
        public Dictionary<Point, Piece> WhitePieces;
        public Dictionary<Point, Piece> BlackPieces;
        public PlayerSide SidePlayable { get; set; } = PlayerSide.White;
        public Tile[,] Tiles { get; set; }
        public bool GameOver { get; set; }

        public Board()
            : this(new Dictionary<Point, Piece>(),
                   new Dictionary<Point, Piece>(),
                   new Tile[8, 8])
        {

        }

        public Board(Dictionary<Point, Piece> _whitePieces,
                     Dictionary<Point, Piece> _blackPieces,
                     Tile[,] _tiles)
        {
            Tiles = _tiles;
            WhitePieces = _whitePieces;
            BlackPieces = _blackPieces;
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
            List<Piece> pieces = WhitePieces.Values.ToList();

            if (SidePlayable == PlayerSide.Black)
            {
                pieces = BlackPieces.Values.ToList();
            }

            return pieces;
        }

        public Board Copy()
        {
            Tile[,] copiedTiles = new Tile[8, 8];
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    copiedTiles[y, x] = Tiles[y, x].Copy();
                }
            }

            var copiedWhitePieces = new Dictionary<Point, Piece>();
            foreach (var kvp in WhitePieces)
            {
                copiedWhitePieces[kvp.Key] = kvp.Value.Copy();
            }

            var copiedBlackPieces = new Dictionary<Point, Piece>();
            foreach (var kvp in BlackPieces)
            {
                copiedBlackPieces[kvp.Key] = kvp.Value.Copy();
            }

            Board copiedBoard = new Board(copiedWhitePieces, copiedBlackPieces, copiedTiles)
            {
                SidePlayable = this.SidePlayable,
                GameOver = this.GameOver,
            };

            return copiedBoard;
        }
    }
}
