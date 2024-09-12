namespace Knightmare.Pieces
{
    internal abstract class Bishop : ChessPiece
    {
        private static readonly int[,] moveSet = new int[,] { { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } };
        private const int pieceValue = 3;

        protected Bishop(char notation, string shape, Point position, PlayerSide side)
            : base(notation, shape, position, moveSet, side, pieceValue)
        {

        }

        public static ChessPiece Create(Point position, PlayerSide side)
        {
            switch (side)
            {
                case PlayerSide.White:
                    return new WhiteBishop(position);
                case PlayerSide.Black:
                    return new BlackBishop(position);
                default:
                    return new Piece();
            }
        }
    }
}
