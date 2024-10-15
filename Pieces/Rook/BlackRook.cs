namespace Knightmare.Pieces
{
    internal sealed class BlackRook : Rook
    {
        private const char notation = 'r'; 
        private const string shape = " \u265C";
        private const PlayerSide side = PlayerSide.Black;

        public BlackRook(Point position)
            : base(notation, shape, position, side)
        {

        }
    }
}
