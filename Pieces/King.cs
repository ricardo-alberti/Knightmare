using Knightmare.Moves;
using Knightmare.Boards;

namespace Knightmare.Pieces
{
    internal class King : Piece
    {
        private const int kingValue = 500;
        private static readonly int[,] moveSet = new int[,]
        {
            { 0, -1 }, { 0, 1 }, { 1, 0 }, { 1, 1 },
            { -1, 0 }, { -1, -1 }, { 1, -1 } , { -1, 1 }
        };

        protected King(char notation, char shape, Point position, PlayerSide side)
            : base(notation, shape, position, moveSet, side, kingValue)
        {

        }

        public static Piece Create(Point position, PlayerSide side)
        {
            char notation = side == PlayerSide.White ? 'K' : 'k';
            char shape = side == PlayerSide.White ? '\u2654' : '\u265A';
            return new King(notation, shape, position, side);
        }

        public override List<Move> MoveRange(Board _position)
        {
            List<Move> moveRange = new List<Move>();
            Tile initialTile = _position.Tile(Position.x, Position.y);

            for (int i = 0; i < moveSet.GetLength(0); ++i)
            {
                int dx = moveSet[i, 0];
                int dy = moveSet[i, 1];
                int x = Position.x + dx;
                int y = Position.y + dy;

                if (!InsideBounds(x, y))
                    continue;

                Tile finalTile = _position.Tile(x, y);
                if (finalTile.TilePiece != null && finalTile.TilePiece.Side == Side)
                    continue;

                moveRange.Add(new Move(initialTile, finalTile));
            }

            return moveRange;
        }
    }
}
