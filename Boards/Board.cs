using Knightmare.Moves;
using Knightmare.Pieces;

namespace Knightmare.Boards
{
    internal class Board
    {
        public Dictionary<Point, Piece> WhitePieces;
        public Dictionary<Point, Piece> BlackPieces;
        public PlayerSide SidePlayable { get; set; } = PlayerSide.White;
        public Tile[,] Tiles { get; set; }
        public Stack<Move> History { get; set; }

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
            History = new Stack<Move>();
        }

        public void Undo()
        {
            History.Pop().Undo(this);
        }

        public Tile Tile(int x, int y)
        {
            return Tiles[y, x];
        }

        public Tile Tile(Point point)
        {
            return Tiles[point.y, point.x];
        }

        public Dictionary<Point, Piece> SidePieces()
        {
            Dictionary<Point, Piece> pieces = WhitePieces;

            if (SidePlayable == PlayerSide.Black)
            {
                pieces = BlackPieces;
            }

            return pieces;
        }
    }
}
