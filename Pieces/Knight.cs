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
            int[,] moveSet = MoveSet;
            int moveset_x, moveset_y, finaltile_x, finaltile_y;

            for (int i = 0; i < moveSet.GetLength(0); ++i)
            {
                moveset_x = moveSet[i, 1];
                moveset_y = moveSet[i, 0];

                finaltile_x = initialTile.Position.x + moveset_x;
                finaltile_y = initialTile.Position.y + moveset_y;

                if (finaltile_x < 0 || finaltile_y < 0 || finaltile_y > 7 || finaltile_x > 7) continue;

                Tile finalTile = _position.Tile(finaltile_x, finaltile_y);

                if (finalTile.TilePiece != null && finalTile.TilePiece.Side == initialTile.TilePiece?.Side) continue;

                moveRange.Add(new Move(initialTile, finalTile));
            }

            return moveRange;
        }
    }
}
