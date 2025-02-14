using Knightmare.Moves;
using Knightmare.Boards;

namespace Knightmare.Pieces
{
    internal abstract class King : Piece
    {
        private const int kingValue = 500;
        private static readonly int[,] moveSet = new int[,] {
            { 0, -1 }, { 0, 1 }, { 1, 0 }, { 1, 1 },
            { -1, 0 }, { -1, -1 }, { 1, -1 } , { -1, 1 }
        };

        protected King(char notation, string shape, Point position, PlayerSide side)
            : base(notation, shape, position, moveSet, side, kingValue)
        {

        }

        public static Piece Create(Point position, PlayerSide side)
        {
            if (side == PlayerSide.White)
                return new WhiteKing(position);

            return new BlackKing(position);
        }

        public override List<Move> MoveRange(Board _position)
        {
            List<Move> moveRange = new List<Move>();

            Tile initialTile = _position.Tile(Position.x, Position.y);
            int[,] moveSet = MoveSet;
            int moveset_x, moveset_y, finaltile_x, finaltile_y;

            for (int i = 0; i < moveSet.GetLength(0); ++i)
            {
                moveset_x = moveSet[i, 1];
                moveset_y = moveSet[i, 0];

                finaltile_x = initialTile.Position.x + moveset_x;
                finaltile_y = initialTile.Position.y + moveset_y;

                if (finaltile_x < 0 || finaltile_y < 0 || finaltile_y > 7 || finaltile_x > 7)
                    continue;

                Tile finalTile = _position.Tile(finaltile_x, finaltile_y);

                if (finalTile.TilePiece != null && finalTile.TilePiece.Side == Side)
                    continue;

                moveRange.Add(new Move(initialTile, finalTile));
            }

            return moveRange;
        }
    }
}
