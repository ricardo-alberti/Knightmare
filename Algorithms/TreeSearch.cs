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

        public abstract MoveTree BestTree(Board _position, Robot me, Robot enemy, int level);

        public List<Move> GenerateMoves(Board _position, Robot robot)
        {
            List<Move> allMoves = new List<Move>();
            List<ChessPiece> pieces = _position.SidePieces(robot.Side()).Values.ToList();

            foreach (ChessPiece piece in pieces)
            {
                allMoves.AddRange(piece.MoveRange(_position));
            }

            return allMoves;
        }
    }
}
