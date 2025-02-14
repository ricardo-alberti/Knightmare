namespace Knightmare.Pieces
{
    internal sealed class WhitePawn : Pawn
    {
        private const char notation = 'P';
        private const string shape = " \u2659";
        private static readonly int[,] moveSet = new int[,] { { -1, 1 }, { -1, -1 }, { -1, 0 } };
        private const PlayerSide side = PlayerSide.White;

        public WhitePawn(Point position)
            : base(notation, shape, position, moveSet, side)
        {

        }
    }
}
