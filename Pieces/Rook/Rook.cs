namespace Knightmare.Pieces
{
    internal abstract class Rook : ChessPiece
    {
        private static readonly int[,] moveSet = new int[,] { { -1, 0 }, { 1, 0 }, { 0, 1 }, { 0, -1 } };
        private const int pieceValue = 5;

        protected Rook(char notation, string shape, Point position, PlayerSide side)
            : base(notation, shape, position, moveSet, side, pieceValue)
        {

        }

        public static ChessPiece Create(Point position, PlayerSide side)
        {
            switch (side)
            {
                case PlayerSide.White:
                    return new WhiteRook(position);
                case PlayerSide.Black:
                    return new BlackRook(position);
                default:
                    return new Piece();
            }
        }
    }
}
