using System.Collections.Concurrent;
using Knightmare.Moves;
using Knightmare.Boards;
using System.Diagnostics;

namespace Knightmare.Algorithm
{
    internal class MinimaxAlphaBeta : TreeSearch
    {
        public MinimaxAlphaBeta() : this(new SimpleEvaluation()) { }
        public MinimaxAlphaBeta(IEvaluation _evaluator) : base(_evaluator)
        {

        }

        public override MoveTree BestTree(Board _position, int level)
        {
            var watch = Stopwatch.StartNew();

            bool isMaximizing = _position.SidePlayable == PlayerSide.White;
            var bestMoveTree = EvaluateTrees(_position, level, int.MinValue, int.MaxValue, isMaximizing);

            watch.Stop();
            ElapsedTime = watch.ElapsedMilliseconds;

            return bestMoveTree;
        }

        private MoveTree EvaluateTrees(Board _position, int depth, int alpha, int beta, bool isMaximizing)
        {
            if (depth == 0)
            {
                int eval = evaluator.Execute(_position);
                return new MoveTree(new Node() { eval = eval });
            }

            List<Move> possibleMoves = base.GenerateMoves(_position);
            if (possibleMoves.Count == 0)
            {
                int eval = evaluator.Execute(_position);
                return new MoveTree(new Node() { eval = eval });
            }

            Node rootNode = new Node() { eval = isMaximizing ? alpha : beta };
            MoveTree bestMoveTree = new MoveTree(rootNode);

            foreach (var move in possibleMoves)
            {
                TotalMoves++;
                Board newPosition = _position.Copy();
                newPosition.Update(move);

                MoveTree childTree = EvaluateTrees(newPosition, depth - 1, alpha, beta, !isMaximizing);
                int childEval = childTree.Root().eval;

                Node childNode = new Node() { value = move, eval = childEval };
                rootNode.AddChild(childNode);

                if (isMaximizing)
                {
                    if (childEval > rootNode.eval)
                    {
                        rootNode.value = move;
                        rootNode.eval = childEval;
                    }
                    alpha = Math.Max(alpha, childEval);
                }
                else
                {
                    if (childEval < rootNode.eval)
                    {
                        rootNode.value = move;
                        rootNode.eval = childEval;
                    }
                    beta = Math.Min(beta, childEval);
                }

                if (beta <= alpha)
                {
                    break;
                }
            }

            return bestMoveTree;
        }
    }
}
