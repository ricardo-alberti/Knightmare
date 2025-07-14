namespace Knightmare.Pieces
{
    internal class Queen : Piece
    {
        private const int pieceValue = 9;
        private static readonly int[,] moveSet = new int[,]
        { 
            { 0, -1 }, { 0, 1 }, { 1, 0 }, { 1, 1 }, 
            { -1, 0 }, { -1, -1 }, { 1, -1 }, { -1, 1 } 
        };

        protected Queen(char notation, char shape, Point position, PlayerSide side)
            : base(notation, shape, position, moveSet, side, pieceValue)
        {

        }

        public static Piece Create(Point position, PlayerSide side)
        {
            char notation = side == PlayerSide.White ? 'Q' : 'q';
            char shape = side == PlayerSide.White ? '\u2655' : '\u265B';
            return new Queen(notation, shape, position, side);
        }
    }
}
