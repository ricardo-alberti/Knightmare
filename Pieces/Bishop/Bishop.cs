namespace Knightmare.Pieces
{
    internal abstract class Bishop : Piece
    {
        private static readonly int[,] moveSet = new int[,] { { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } };
        private const int pieceValue = 3;

        protected Bishop(char notation, string shape, Point position, PlayerSide side)
            : base(notation, shape, position, moveSet, side, pieceValue)
        {

        }

        public static Piece Create(Point position, PlayerSide side)
        {
            if (side == PlayerSide.White)
            {
                return new WhiteBishop(position);
            }

            return new BlackBishop(position);
        }
    }
}
