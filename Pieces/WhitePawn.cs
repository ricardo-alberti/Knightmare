namespace Knightmare.Pieces
{
    internal sealed class WhitePawn : Pawn
    {
        private const char notation = 'P';
        private const char shape = '\u2659';
        private const PlayerSide side = PlayerSide.White;
        private static readonly int[,] moveSet = new int[,] 
        { 
            { -1, 1 }, { -1, -1 }, { -1, 0 } 
        };

        public WhitePawn(Point position)
            : base(notation, shape, position, moveSet, side)
        {

        }
    }
}
