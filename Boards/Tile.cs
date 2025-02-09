using Knightmare.Pieces;

namespace Knightmare.Boards
{
    internal class Tile
    {
        private readonly Point position;
        private readonly Piece piece;

        public Tile() : this(null, new Point(0, 0)) { }

        public Tile(Point _point) : this(null, _point) { }

        public Tile(Piece _piece, Point _point)
        {
            position = _point;
            piece = _piece;
        }

        public Piece Piece()
        {
            return piece;
        }

        public Tile SetPiece(Piece _piece)
        {
            if (_piece == null)
            {
                return new Tile(position);
            }

            return new Tile(_piece.UpdatePosition(Position()), position);
        }

        public Point Position()
        {
            return position;
        }
    }
}
