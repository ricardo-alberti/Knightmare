namespace Knightmare.Pieces
{
    internal sealed class BlackPawn : Pawn
    {
        private const char notation = 'p';
        private const char shape = '\u265F';
        private const PlayerSide side = PlayerSide.Black;
        private static readonly int[,] moveSet = new int[,] 
        { 
            { 1, -1 }, { 1, 1 }, { 1, 0 } 
        };

        public BlackPawn(Point position)
            : base(notation, shape, position, moveSet, side)
        {

        }
    }
}
