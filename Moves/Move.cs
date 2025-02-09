using Knightmare.Pieces;
using Knightmare.Boards;

namespace Knightmare.Moves
{
    internal sealed class Move
    {
        private readonly Piece piece;
        private readonly Piece captured;
        private readonly Tile initialTile;
        private readonly Tile finalTile;

        public Move() : this(new Tile(new Point(0, 0)), new Tile(new Point(0, 0))) { }

        public Move(Tile _initialTile, Tile _finalTile)
        {
            piece = _initialTile.Piece();
            captured = _finalTile.Piece();
            initialTile = _initialTile.SetPiece(null);
            finalTile = _finalTile.SetPiece(piece);
        }

        public static Move Create(string _inputUCI, Board _board)
        {
            //a2a3
            Move move = new();
            string input = _inputUCI.ToLower();

            int firstX = 7 - (input[0] - 'a');
            int firstY = input[1] - '1';
            int lastX = 7 - (input[2] - 'a');
            int lastY = input[3] - '1';

            move = new Move(_board.Tile(firstX, firstY), _board.Tile(lastX, lastY));

            return move;
        }

        public Tile[] Tiles()
        {
            return new Tile[2] { initialTile, finalTile };
        }

        public Piece Piece()
        {
            return piece;
        }

        public Piece Captured()
        {
            return captured;
        }

        public char Notation()
        {
            return piece.Notation();
        }
    }
}
