namespace Knightmare.Pieces
{
    internal sealed class BlackBishop : Bishop
    {
        private const char notation = 'b'; 
        private const string shape = " \u265D";
        private const PlayerSide side = PlayerSide.Black;

        public BlackBishop(Point position)
            : base(notation, shape, position, side)
        {

        }
    }
}
