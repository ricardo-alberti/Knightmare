using Knightmare.Moves;

namespace Knightmare.DTO
{
    internal class CalculationResponse
    {
        private readonly int calculatedMoves;
        private readonly MoveTree bestMoveTree;
        private readonly Move bestMove;
        private readonly long elapsedTime;
        private readonly int evaluation;

        public CalculationResponse() : this(0, new MoveTree(), new Move(), 0, 0)
        {

        }

        public CalculationResponse(int _calculatedMoves, MoveTree _bestMoveTree, Move _bestMove, long _elapsedTime, int _evaluation)
        {
            calculatedMoves = _calculatedMoves;
            bestMoveTree = _bestMoveTree;
            bestMove = _bestMove;
            elapsedTime = _elapsedTime;
            evaluation = _evaluation;
        }

        public Move BestMove()
        {
            return bestMove;
        }

        public int CalculatedMovesTotal()
        {
            return calculatedMoves;
        }

        public long ElapsedTime()
        {
            return elapsedTime;
        }

        public int Evaluation()
        {
            return evaluation;
        }

        public MoveTree MoveTree()
        {
            return bestMoveTree;
        }
    }
}
