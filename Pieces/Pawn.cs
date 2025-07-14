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
            int[,] moveSet = MoveSet;
            Tile initialTile = _position.Tile(Position.x, Position.y);

            if (initialTile.TilePiece == null) return new List<Move>();

            int moveset_x, moveset_y, finaltile_x, finaltile_y;

            for (int i = 0; i < moveSet.GetLength(0); ++i)
            {
                moveset_x = moveSet[i, 1];
                moveset_y = moveSet[i, 0];

                finaltile_x = initialTile.Position.x + moveset_x;
                finaltile_y = initialTile.Position.y + moveset_y;

                if (finaltile_x < 0 || finaltile_y < 0 || finaltile_y > 7 || finaltile_x > 7) continue;

                Tile finalTile = _position.Tile(finaltile_x, finaltile_y);

                if (finalTile.TilePiece != null && finalTile.TilePiece.Side == initialTile.TilePiece.Side) continue;
                if (moveset_x != 0 && finalTile.TilePiece == null) continue;
                if (moveset_x == 0 && finalTile.TilePiece != null) continue;

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
