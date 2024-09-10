using Knightmare.Moves;
using Knightmare.Boards;

namespace Knightmare.Pieces
{
    internal abstract class Knight : ChessPiece
    {
        private static readonly int[,] moveSet = new int[,] { { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 }, { 1, 2 }, { -1, 2 }, { 1, -2 }, { -1, -2 } };
        private const int pieceValue = 3;

        protected Knight(int id, char notation, string shape, Point position, PlayerSide side)
            : base(id, notation, shape, position, moveSet, side, pieceValue)
        {

        }

        public new static ChessPiece Create(Point position, PlayerSide side, int id)
        {
            switch (side)
            {
                case PlayerSide.White:
                    return new WhiteKnight(position, id);
                case PlayerSide.Black:
                    return new BlackKnight(position, id);
                default:
                    return new Piece();
            }
        }

        public override List<Move> MoveRange(Board _position)
        {
            List<Move> moveRange = new List<Move>();

            Tile initialTile = _position.Tile(Position().x, Position().y);
            ChessPiece piece = initialTile.Piece();
            int[,] moveSet = piece.MoveSet();
            int moveset_x, moveset_y, finaltile_x, finaltile_y;

            for (int i = 0; i < moveSet.GetLength(0); ++i)
            {
                moveset_x = moveSet[i, 1];
                moveset_y = moveSet[i, 0];

                finaltile_x = initialTile.Position().x + moveset_x;
                finaltile_y = initialTile.Position().y + moveset_y;

                if (finaltile_x < 0 || finaltile_y < 0 || finaltile_y > 7 || finaltile_x > 7) continue;

                Tile finalTile = _position.Tile(finaltile_x, finaltile_y);

                if (finalTile.Piece().Side() == initialTile.Piece().Side()) continue;

                moveRange.Add(new Move(initialTile, finalTile));
            }

            return moveRange;
        }
    }
}
