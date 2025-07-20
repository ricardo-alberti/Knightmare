using Knightmare.Moves;
using Knightmare.Boards;

namespace Knightmare.Algorithm
{
    internal class AlphaBeta : TreeSearch
    {
        public AlphaBeta() : this(new SimpleEvaluation()) { }
        public AlphaBeta(IEvaluation _evaluator) : base(_evaluator) { }

        public override Node BestTree(Board _position, int depth)
        {
            bool isMaximizing = _position.SideToMove == PlayerSide.White;
            return EvaluateBestContinuation(_position, depth, int.MinValue,
                    int.MaxValue, isMaximizing);
        }

        private Node EvaluateBestContinuation(Board _position, int depth,
                int alpha, int beta, bool isMaximizing)
        {
            ulong key = _position.Hash();

            if (TranspositionTable.TryGet(key, out var entry) && entry!.Depth >= depth)
            {
                switch (entry.Bound)
                {
                    case BoundType.Exact:
                        return new Node { Eval = entry.Eval, Value = entry.BestMove };
                    case BoundType.LowerBound when entry.Eval > alpha:
                        alpha = entry.Eval;
                        break;
                    case BoundType.UpperBound when entry.Eval < beta:
                        beta = entry.Eval;
                        break;
                }

                if (beta <= alpha)
                {
                    return new Node { Eval = entry.Eval };
                }
            }

            if (depth == 0 || _position.Terminal)
            {
                return new Node() { Eval = evaluator.Execute(_position) };
            }

            List<Move> possibleMoves = GenerateMoves(_position);
            if (possibleMoves.Count == 0)
            {
                return new Node() { Eval = evaluator.Execute(_position) };
            }

            int alphaOrig = alpha;
            int betaOrig = beta;
            Move? bestMove = null;

            Node rootNode = new Node()
            {
                Eval = isMaximizing ? int.MinValue : int.MaxValue,
            };

            foreach (var move in possibleMoves)
            {
                move.Execute(_position);

                ++TotalMovesEvaluated;

                Node childTree = EvaluateBestContinuation(_position, 
                        depth - 1, alpha, beta, !isMaximizing);
                int childEval = childTree.Eval;

                move.Undo(_position);

                if (isMaximizing)
                {
                    if (childEval > rootNode.Eval)
                    {
                        rootNode.Eval = childEval;
                        bestMove = move;
                    }
                    alpha = Math.Max(alpha, childEval);
                }
                else
                {
                    if (childEval < rootNode.Eval)
                    {
                        rootNode.Eval = childEval;
                        bestMove = move;
                    }
                    beta = Math.Min(beta, childEval);
                }

                if (beta <= alpha)
                {
                    break;
                }
            }

            BoundType bound;
            if (rootNode.Eval <= alphaOrig) bound = BoundType.UpperBound;
            else if (rootNode.Eval >= beta) bound = BoundType.LowerBound;
            else bound = BoundType.Exact;

            TranspositionTable.Store(key, new TranspositionEntry(
                        rootNode.Eval, depth, bound, bestMove));

            rootNode.Value = bestMove;
            return rootNode;
        }
    }
}
