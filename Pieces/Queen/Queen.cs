namespace Knightmare.Pieces
{
    internal abstract class Queen : Piece
    {
        private static readonly int[,] moveSet = new int[,] { { 0, -1 }, { 0, 1 }, { 1, 0 }, { 1, 1 }, { -1, 0 }, { -1, -1 }, { 1, -1 }, { -1, 1 } };
        private const int pieceValue = 9;

        protected Queen(char notation, string shape, Point position, PlayerSide side)
            : base(notation, shape, position, moveSet, side, pieceValue)
        {

        }

        public static Piece Create(Point position, PlayerSide side)
        {
            if (side == PlayerSide.White)
                return new WhiteQueen(position);

            return new BlackQueen(position);
        }
    }
}
