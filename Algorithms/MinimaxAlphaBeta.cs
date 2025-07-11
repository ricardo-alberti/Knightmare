using Knightmare.Moves;
using Knightmare.Boards;

namespace Knightmare.Algorithm
{
    internal class MinimaxAlphaBeta : TreeSearch
    {
        public MinimaxAlphaBeta() : this(new SimpleEvaluation()) { }
        public MinimaxAlphaBeta(IEvaluation _evaluator) : base(_evaluator)
        {

        }

        public override Node BestTree(Board _position, int depth)
        {
            bool isMaximizing = _position.SidePlayable == PlayerSide.White;
            return EvaluateBestContinuation(_position, depth, int.MinValue, int.MaxValue, isMaximizing);
        }

        private Node EvaluateBestContinuation(Board _position, int depth, int alpha, int beta, bool isMaximizing)
        {
            int currentEval = evaluator.Execute(_position);
            if (depth == 0)
            {
                return new Node() { Eval = currentEval };
            }

            List<Move> possibleMoves = GenerateMoves(_position);
            if (possibleMoves.Count == 0)
            {
                return new Node() { Eval = currentEval };
            }

            Node rootNode = new Node() {
                Eval = isMaximizing ? int.MinValue : int.MaxValue,
            };

            Node? bestMoveTree = null;
            Move? bestMove = null;

            foreach (var move in possibleMoves)
            {
                Board position = _position.Copy();
                move.Execute(position);

                Node childTree = EvaluateBestContinuation(position, depth - 1, alpha, beta, !isMaximizing);
                childTree.Value = move;
                childTree.Position = position;

                int childEval = childTree.Eval;

                if (isMaximizing)
                {
                    if (childEval > rootNode.Eval)
                    {
                        rootNode.Eval = childEval;
                        bestMoveTree = childTree;
                        bestMove = move;
                    }
                    alpha = Math.Max(alpha, childEval);
                }
                else
                {
                    if (childEval < rootNode.Eval)
                    {
                        rootNode.Eval = childEval;
                        bestMoveTree = childTree;
                        bestMove = move;
                    }
                    beta = Math.Min(beta, childEval);
                }

                if (beta <= alpha)
                {
                    break;
                }
            }

            if (bestMoveTree == null)
            {
                bestMoveTree = new Node() { Eval = rootNode.Eval, Position = _position.Copy() };
            }

            rootNode.Value = bestMove;
            rootNode.Subnodes.Add(bestMoveTree);
            return rootNode;
        }
    }
}
