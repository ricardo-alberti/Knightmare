using Knightmare.Moves;
using Knightmare.Pieces;
using Knightmare.Boards;

namespace Knightmare.Algorithm
{
    abstract internal class TreeSearch : ITreeSearch
    {
        public long TotalMovesEvaluated { get; set; }
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
                allMoves.AddRange(piece.MoveRange(_position));
            }

            allMoves.Sort((a, b) =>
            {
                bool aCapture = a.IsCapture();
                bool bCapture = b.IsCapture();

                if (aCapture && bCapture)
                {
                    int aVictimValue = a.CapturedPiece?.Value ?? 0;
                    int aAttackerValue = a.MovingPiece?.Value ?? 0;

                    int bVictimValue = b.CapturedPiece?.Value ?? 0;
                    int bAttackerValue = b.MovingPiece?.Value ?? 0;

                    int aScore = aVictimValue * 10 - aAttackerValue;
                    int bScore = bVictimValue * 10 - bAttackerValue;

                    return bScore.CompareTo(aScore);
                }

                if (aCapture) return -1;
                if (bCapture) return 1;

                return 0;
            });

            return allMoves;
        }
    }
}
