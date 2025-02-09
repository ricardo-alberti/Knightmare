namespace Knightmare.Pieces
{
    internal abstract class Rook : Piece
    {
        private static readonly int[,] moveSet = new int[,] { { -1, 0 }, { 1, 0 }, { 0, 1 }, { 0, -1 } };
        private const int pieceValue = 5;

        protected Rook(char notation, string shape, Point position, PlayerSide side)
            : base(notation, shape, position, moveSet, side, pieceValue)
        {

        }

        public static Piece Create(Point position, PlayerSide side)
        {
            if (side == PlayerSide.White)
                return new WhiteRook(position);

            return new BlackRook(position);
        }
    }
}
