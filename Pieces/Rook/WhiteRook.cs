namespace Knightmare.Pieces
{
    internal sealed class WhiteRook : Rook
    {
        private const char notation = 'R'; 
        private const string shape = " \u2656";
        private const PlayerSide side = PlayerSide.White;

        public WhiteRook(Point position, int id)
            : base(id, notation, shape, position, side)
        {

        }
    }
}
