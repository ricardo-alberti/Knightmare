namespace Knightmare.Pieces
{
    internal class Bishop : Piece
    {
        private const int pieceValue = 3;
        private static readonly int[,] moveSet = new int[,] 
        { 
            { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } 
        };

        protected Bishop(char notation, char shape, Point position, PlayerSide side)
            : base(notation, shape, position, moveSet, side, pieceValue)
        {

        }

        public static Piece Create(Point position, PlayerSide side)
        {
            char notation = side == PlayerSide.White ? 'B' : 'b';
            char shape = side == PlayerSide.White ? '\u2657' : '\u265D';
            return new Bishop(notation, shape, position, side);
        }
    }
}
