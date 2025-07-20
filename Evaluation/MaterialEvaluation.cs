using System.Numerics;

internal class MaterialEvaluator : IEvaluator
{
    public int Execute(Board _position)
    {
        return (BitOperations.PopCount(_position.WhitePawns)
                - BitOperations.PopCount(_position.BlackPawns))
                + (BitOperations.PopCount(_position.WhiteBishops) * 3
                - BitOperations.PopCount(_position.BlackBishops) * 3)
                + (BitOperations.PopCount(_position.WhiteRooks) * 5
                - BitOperations.PopCount(_position.BlackRooks) * 5)
                + (BitOperations.PopCount(_position.WhiteQueens) * 9
                - BitOperations.PopCount(_position.BlackQueens) * 9)
                + (BitOperations.PopCount(_position.WhiteKnights) * 3
                - BitOperations.PopCount(_position.BlackKnights) * 3);
    }
}
