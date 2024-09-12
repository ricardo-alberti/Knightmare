namespace Knightmare.Pieces
{
    internal sealed class WhiteQueen : Queen
    {
        private const char notation = 'Q'; 
        private const string shape = " \u2655";
        private const PlayerSide side = PlayerSide.White;

        public WhiteQueen(Point position)
            : base(notation, shape, position, side)
        {

        }
    }
}
