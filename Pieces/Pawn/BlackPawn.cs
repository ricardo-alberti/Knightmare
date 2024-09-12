namespace Knightmare.Pieces
{
    internal sealed class BlackPawn : Pawn
    {
        private const char notation = 'p'; 
        private const string shape = " \u265F";
        private static readonly int[,] moveSet = new int[,] { { -1, -1 }, { -1, 1 }, { -1, 0 } }; 
        private const PlayerSide side = PlayerSide.Black;

        public BlackPawn(Point position)
            : base(notation, shape, position, moveSet, side)
        {

        }
    }
}
