using Knightmare.Moves;
using Knightmare.Boards;

namespace Knightmare.Pieces
{
    internal abstract class Pawn : ChessPiece
    {
        private const int pieceValue = 1;

        protected Pawn(char notation, string shape, Point position, int[,] moveSet, PlayerSide side)
            : base(notation, shape, position, moveSet, side, pieceValue)
        {

        }

        public static ChessPiece Create(Point position, PlayerSide side)
        {
            switch (side)
            {
                case PlayerSide.White:
                    return new WhitePawn(position);
                case PlayerSide.Black:
                    return new BlackPawn(position);
                default:
                    return new Piece();
            }
        }

        public override List<Move> MoveRange(Board _position)
        {
            Board position = _position.Copy();
            List<Move> moveRange = new List<Move>();

            Tile initialTile = position.Tile(Position().x, Position().y);
            ChessPiece piece = initialTile.Piece();
            int[,] moveSet = piece.MoveSet();

            int moveset_x, moveset_y, finaltile_x, finaltile_y;
            int offset = (piece.Position().y == 6 || piece.Position().y == 1) ? 0 : 1;

            for (int i = 0; i < moveSet.GetLength(0) - offset; ++i)
            {
                moveset_x = moveSet[i, 1];
                moveset_y = moveSet[i, 0];

                finaltile_x = initialTile.Position().x + moveset_x;
                finaltile_y = initialTile.Position().y + moveset_y;

                if (finaltile_x < 0 || finaltile_y < 0 || finaltile_y > 7 || finaltile_x > 7) continue;

                Tile finalTile = position.Tile(finaltile_x, finaltile_y);

                if (finalTile.Piece().Side() == initialTile.Piece().Side()) continue;
                if (moveset_x != 0 && finalTile.Piece().Side() == PlayerSide.None) continue;
                if (moveset_x == 0 && finalTile.Piece().Side() != PlayerSide.None) continue;

                if (finalTile.Position().y == 7 || finalTile.Position().y == 0)
                {
                    char[] promotions = { 'Q', 'N', 'B', 'R' };

                    foreach (char promotion in promotions)
                    {
                        piece = Promote('Q');
                        initialTile = initialTile.SetPiece(piece);
                        moveRange.Add(new Move(initialTile, finalTile));
                    }

                    continue;
                }

                moveRange.Add(new Move(initialTile, finalTile));
            }

            return moveRange;
        }

        private ChessPiece Promote(char notation)
        {
            return Create(notation, base.Position(), base.Side());
        }
    }
}
