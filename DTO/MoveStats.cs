using Knightmare.Moves;

namespace Knightmare.DTO
{
    internal class MoveStats
    {
        private readonly int calculatedMoves;
        private readonly int evaluation;
        private readonly long elapsedTime;
        private readonly Move move;
        private readonly MoveTree moveTree;

        public MoveStats(int _calculatedMoves, MoveTree _moveTree,
                Move _move, long _elapsedTime, int _evaluation)
        {
            calculatedMoves = _calculatedMoves;
            moveTree = _moveTree;
            move = _move;
            elapsedTime = _elapsedTime;
            evaluation = _evaluation;
        }

        public Move Move()
        {
            return move;
        }

        public int CalculatedMoves()
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
            return moveTree;
        }
    }
}
