namespace Knightmare.Pieces
{
    internal sealed class BlackKing : King
    {
        private const char notation = 'k'; 
        private const string shape = " \u265A";
        private const PlayerSide side = PlayerSide.Black;

        public BlackKing(Point position, int id)
            : base(id, notation, shape, position, side)
        {

        }
    }
}
