using System.Numerics;

internal class MaterialEvaluator : IEvaluator
{
    public int Execute(Board position)
    {
        return (BitOperations.PopCount(position.WhitePawns)
                - BitOperations.PopCount(position.BlackPawns))
                + (BitOperations.PopCount(position.WhiteBishops) * PieceValue.Bishop
                - BitOperations.PopCount(position.BlackBishops) * PieceValue.Bishop)
                + (BitOperations.PopCount(position.WhiteRooks) * PieceValue.Rook
                - BitOperations.PopCount(position.BlackRooks) * PieceValue.Rook)
                + (BitOperations.PopCount(position.WhiteQueens) * PieceValue.Queen
                - BitOperations.PopCount(position.BlackQueens) * PieceValue.Queen)
                + (BitOperations.PopCount(position.WhiteKnights) * PieceValue.Knight
                - BitOperations.PopCount(position.BlackKnights) * PieceValue.Knight)
                + (BitOperations.PopCount(position.WhiteKing) * PieceValue.King
                - BitOperations.PopCount(position.BlackKing) * PieceValue.King);
    }
}
