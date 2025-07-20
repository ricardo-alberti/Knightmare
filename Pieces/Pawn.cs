using Knightmare.Moves;
using Knightmare.Boards;

namespace Knightmare.Pieces
{
    internal abstract class Pawn : Piece
    {
        private const int pieceValue = 1;

        protected Pawn(char notation, char shape, Point position, int[,] moveSet, PlayerSide side)
            : base(notation, shape, position, moveSet, side, pieceValue)
        {

        }

        public static Piece Create(Point position, PlayerSide side)
        {
            if (side == PlayerSide.White)
                return new WhitePawn(position);

            return new BlackPawn(position);
        }

        public override List<Move> MoveRange(Board _position)
        {
            List<Move> moveRange = new List<Move>();
            Tile initialTile = _position.Tile(Position.x, Position.y);

            if (initialTile.TilePiece == null) return new List<Move>();

            for (int i = 0; i < MoveSet.GetLength(0); ++i)
            {
                int dx = MoveSet[i, 0];
                int dy = MoveSet[i, 1];
                int x = Position.x + dx;
                int y = Position.y + dy;

                if (!InsideBounds(x, y))
                {
                    continue;
                }

                Tile finalTile = _position.Tile(x, y);

                if (finalTile.TilePiece != null 
                    && finalTile.TilePiece.Side == initialTile.TilePiece.Side) 
                {
                    continue;
                }

                if (x != 0 && finalTile.TilePiece == null) continue;
                if (x == 0 && finalTile.TilePiece != null) continue;
                if (finalTile.Position.y == 7 || finalTile.Position.y == 0)
                {
                    char[] promotions = { 'Q', 'N', 'B', 'R' };

                    foreach (char promotion in promotions)
                    {
                        initialTile.TilePiece = Promote(promotion);
                        moveRange.Add(new Move(initialTile, finalTile));
                    }

                    continue;
                }

                moveRange.Add(new Move(initialTile, finalTile));
            }

            return moveRange;
        }

        public Piece Promote(char notation)
        {
            return Create(notation, Position, Side);
        }
    }
}
