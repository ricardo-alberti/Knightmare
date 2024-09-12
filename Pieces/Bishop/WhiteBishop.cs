namespace Knightmare.Pieces
{
    internal sealed class WhiteBishop : Bishop
    {
        private const char notation = 'B'; 
        private const string shape = " \u2657";
        private const PlayerSide side = PlayerSide.White;

        public WhiteBishop(Point position)
            : base(notation, shape, position, side)
        {

        }
    }
}
