namespace Knightmare.Pieces
{
    internal sealed class WhiteKing : King
    {
        private const char notation = 'K'; 
        private const string shape = " \u2654";
        private const PlayerSide side = PlayerSide.White;

        public WhiteKing(Point position, int id)
            : base(id, notation, shape, position, side)
        {

        }
    }
}
