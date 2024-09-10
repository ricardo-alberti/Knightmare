namespace Knightmare.Pieces
{
    internal class Piece : ChessPiece
    {
        private const int id = 0;
        private const char notation = 'F'; 
        private const string shape = " \u2024";
        private const PlayerSide side = PlayerSide.None;
        private const int value = 0;
        private static readonly Point position = new Point();
        private static readonly int[,] moveSet = new int[,] { { 0, 0 } };

        public Piece()
            : base(id, notation, shape, position, moveSet, side, value)
        {

        }
    }
}
