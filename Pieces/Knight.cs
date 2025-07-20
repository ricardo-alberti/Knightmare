using Knightmare.Moves;
using Knightmare.Boards;

namespace Knightmare.Pieces
{
    internal class Knight : Piece
    {
        private const int pieceValue = 3;
        private static readonly int[,] moveSet = new int[,]
        {
            { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 },
            { 1, 2 }, { -1, 2 }, { 1, -2 }, { -1, -2 }
        };

        protected Knight(char notation, char shape, Point position, PlayerSide side)
            : base(notation, shape, position, moveSet, side, pieceValue)
        {

        }

        public static Piece Create(Point position, PlayerSide side)
        {
            char notation = side == PlayerSide.White ? 'N' : 'n';
            char shape = side == PlayerSide.White ? '\u2658' : '\u265E';
            return new Knight(notation, shape, position, side);
        }

        public override List<Move> MoveRange(Board _position)
        {
            List<Move> moveRange = new List<Move>();
            Tile initialTile = _position.Tile(Position);

            for (int i = 0; i < MoveSet.GetLength(0); ++i)
            {
                int dx = moveSet[i, 0];
                int dy = moveSet[i, 1];
                int x = Position.x + dx;
                int y = Position.y + dy;

                if (!InsideBounds(x, y))
                {
                    continue;
                }

                Tile finalTile = _position.Tile(x, y);
                if (finalTile.TilePiece != null 
                    && finalTile.TilePiece.Side == initialTile.TilePiece?.Side)
                {
                    continue;
                }

                moveRange.Add(new Move(initialTile, finalTile));
            }

            return moveRange;
        }
    }
}
