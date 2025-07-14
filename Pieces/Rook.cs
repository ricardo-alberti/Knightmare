namespace Knightmare.Pieces
{
    internal class Rook : Piece
    {
        private const int pieceValue = 5;
        private static readonly int[,] moveSet = new int[,] 
        {
            { -1, 0 }, { 1, 0 }, { 0, 1 }, { 0, -1 }
        };

        protected Rook(char notation, char shape, Point position, PlayerSide side)
            : base(notation, shape, position, moveSet, side, pieceValue)
        {

        }

        public static Piece Create(Point position, PlayerSide side)
        {
            char notation = side == PlayerSide.White ? 'R' : 'r';
            char shape = side == PlayerSide.White ? '\u2656' : '\u265C';
            return new Rook(notation, shape, position, side);
        }
    }
}
