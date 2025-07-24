using System.Numerics;

internal class MaterialEvaluator : IEvaluator
{
    public int Execute(Board position)
    {
        return (BitOperations.PopCount(position.WhitePawns)
                - BitOperations.PopCount(position.BlackPawns))
                + (BitOperations.PopCount(position.WhiteBishops) * 3
                - BitOperations.PopCount(position.BlackBishops) * 3)
                + (BitOperations.PopCount(position.WhiteRooks) * 5
                - BitOperations.PopCount(position.BlackRooks) * 5)
                + (BitOperations.PopCount(position.WhiteQueens) * 9
                - BitOperations.PopCount(position.BlackQueens) * 9)
                + (BitOperations.PopCount(position.WhiteKnights) * 3
                - BitOperations.PopCount(position.BlackKnights) * 3);
    }
}
