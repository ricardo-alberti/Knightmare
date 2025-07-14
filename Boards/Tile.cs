using Knightmare.Pieces;

namespace Knightmare.Boards
{
    internal class Tile
    {
        public Point Position { get; }
        private Piece? _tilePiece;
        public Piece? TilePiece
        {
            get => _tilePiece;
            set
            {
                _tilePiece = value;
                if (_tilePiece != null)
                {
                    _tilePiece.Position = Position;
                }
            }
        }

        public Tile(Point _point, Piece? _piece = null)
        {
            Position = _point;
            TilePiece = _piece;
        }
    }
}
