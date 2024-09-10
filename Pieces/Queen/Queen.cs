namespace Knightmare.Pieces
{
    internal abstract class Queen : ChessPiece
    {
        private static readonly int[,] moveSet = new int[,] { { 0, -1 }, { 0, 1 }, { 1, 0 }, { 1, 1 }, { -1, 0 }, { -1, -1 }, { 1, -1 }, { -1, 1 } };
        private const int pieceValue = 9;

        protected Queen(int id, char notation, string shape, Point position, PlayerSide side)
            : base(id, notation, shape, position, moveSet, side, pieceValue)
        {

        }

        public static ChessPiece Create(Point position, PlayerSide side, int id)
        {
            switch (side)
            {
                case PlayerSide.White:
                    return new WhiteQueen(position, id);
                case PlayerSide.Black:
                    return new BlackQueen(position, id);
                default:
                    return new Piece();
            }
        }
    }
}
