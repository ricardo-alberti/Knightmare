namespace Knightmare.Pieces
{
    internal abstract class Rook : ChessPiece
    {
        private static readonly int[,] moveSet = new int[,] { { -1, 0 }, { 1, 0 }, { 0, 1 }, { 0, -1 } };
        private const int pieceValue = 5;

        protected Rook(int id, char notation, string shape, Point position, PlayerSide side)
            : base(id, notation, shape, position, moveSet, side, pieceValue)
        {

        }

        public static ChessPiece Create(Point position, PlayerSide side, int id)
        {
            switch (side)
            {
                case PlayerSide.White:
                    return new WhiteRook(position, id);
                case PlayerSide.Black:
                    return new BlackRook(position, id);
                default:
                    return new Piece();
            }
        }
    }
}
