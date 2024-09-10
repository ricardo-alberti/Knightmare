namespace Knightmare.Pieces
{
    internal sealed class WhiteKnight : Knight
    {
        private const char notation = 'N'; 
        private const string shape = " \u2658";
        private const PlayerSide side = PlayerSide.White;

        public WhiteKnight(Point position, int id)
            : base(id, notation, shape, position, side)
        {

        }
    }
}
