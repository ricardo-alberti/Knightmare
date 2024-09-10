namespace Knightmare.Pieces
{
    internal sealed class BlackRook : Rook
    {
        private const char notation = 'R'; 
        private const string shape = " \u265C";
        private const PlayerSide side = PlayerSide.Black;

        public BlackRook(Point position, int id)
            : base(id, notation, shape, position, side)
        {

        }
    }
}
