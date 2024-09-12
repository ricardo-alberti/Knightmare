namespace Knightmare.Pieces
{
    internal sealed class BlackQueen : Queen
    {
        private const char notation = 'q'; 
        private const string shape = " \u265B";
        private const PlayerSide side = PlayerSide.Black;

        public BlackQueen(Point position)
            : base(notation, shape, position, side)
        {

        }
    }
}
