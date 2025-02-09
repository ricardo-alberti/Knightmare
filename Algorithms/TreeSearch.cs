using Knightmare.Moves;
using Knightmare.Pieces;
using Knightmare.Boards;

namespace Knightmare.Algorithm
{
    abstract internal class TreeSearch : ITreeSearch
    {
        public int TotalMoves { get; set; }
        public long ElapsedTime { get; set; }
        protected readonly IEvaluation evaluator;

        public TreeSearch(IEvaluation _evaluator)
        {
            TotalMoves = 0;
            ElapsedTime = 0;
            evaluator = _evaluator;
        }

        public abstract MoveTree BestTree(Board _position, int level);

        public List<Move> GenerateMoves(Board _position)
        {
            List<Move> allMoves = new List<Move>();
            List<Piece> pieces = _position.SidePieces().Values.ToList();

            foreach (Piece piece in pieces)
            {
                allMoves.AddRange(piece.MoveRange(_position));
            }

            return allMoves;
        }
    }
}
