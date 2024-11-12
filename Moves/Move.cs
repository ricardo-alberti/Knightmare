using Knightmare.Pieces;
using Knightmare.Boards;

namespace Knightmare.Moves
{
    internal sealed class Move
    {
        private readonly ChessPiece piece;
        private readonly ChessPiece captured;
        private readonly Tile initialTile;
        private readonly Tile finalTile;

        public Move() : this(new Tile(new Point(0, 0)), new Tile(new Point(0, 0))) { }

        public Move(Tile _initialTile, Tile _finalTile)
        {
            piece = _initialTile.Piece();
            captured = _finalTile.Piece();
            initialTile = _initialTile.SetPiece(new Piece());
            finalTile = _finalTile.SetPiece(piece.UpdatePosition(_finalTile.Position()));
        }

        public Tile[] Tiles()
        {
            return new Tile[2] { initialTile, finalTile };
        }

        public ChessPiece Piece()
        {
            return piece;
        }

        public ChessPiece Captured()
        {
            return captured;
        }

        public Move Undo()
        {
            return new Move(finalTile, initialTile);
        }

        public char Notation()
        {
            return piece.Notation();
        }

        public bool ValidPlayer(Player _player)
        {
            bool ret = false;

            if (Piece().Side() == _player.Side())
            {
                ret = true;
            }

            return ret;
        }
    }
}
