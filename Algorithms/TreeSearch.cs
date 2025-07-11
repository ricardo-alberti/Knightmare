using Knightmare.Moves;
using Knightmare.Pieces;
using Knightmare.Boards;

namespace Knightmare.Algorithm
{
    abstract internal class TreeSearch : ITreeSearch
    {
        protected readonly IEvaluation evaluator;

        public TreeSearch(IEvaluation _evaluator)
        {
            evaluator = _evaluator;
        }

        public abstract Node BestTree(Board _position, int level);

        public List<Move> GenerateMoves(Board _position)
        {
            List<Move> allMoves = new List<Move>();
            List<Piece> pieces = _position.SidePieces();

            foreach (Piece piece in pieces)
            {
                List<Move> pieceRange = piece.MoveRange(_position);
                if (pieceRange.Count > 0)
                {
                    allMoves.AddRange(pieceRange);
                }
            }

            return allMoves;
        }
    }
}
