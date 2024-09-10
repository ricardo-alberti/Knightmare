namespace Knightmare.Pieces
{
    internal abstract class Bishop : ChessPiece
    {
        private static readonly int[,] moveSet = new int[,] { { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } };
        private const int pieceValue = 3;

        protected Bishop(int id, char notation, string shape, Point position, PlayerSide side)
            : base(id, notation, shape, position, moveSet, side, pieceValue)
        {

        }

        public static ChessPiece Create(Point position, PlayerSide side, int id)
        {
            switch (side)
            {
                case PlayerSide.White:
                    return new WhiteBishop(position, id);
                case PlayerSide.Black:
                    return new BlackBishop(position, id);
                default:
                    return new Piece();
            }
        }
    }
}
