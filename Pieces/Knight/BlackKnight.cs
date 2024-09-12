namespace Knightmare.Pieces
{
    internal sealed class BlackKnight : Knight
    {
        private const char notation = 'n'; 
        private const string shape = " \u265E";
        private const PlayerSide side = PlayerSide.Black;

        public BlackKnight(Point position)
            : base(notation, shape, position, side)
        {

        }
    }
}
