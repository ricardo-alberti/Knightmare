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
            var bestMoveTree = EvaluateBestContinuation(_position, level, int.MinValue, int.MaxValue, isMaximizing);

            watch.Stop();
            ElapsedTime = watch.ElapsedMilliseconds;

            return bestMoveTree;
        }

        private MoveTree EvaluateBestContinuation(Board _position, int depth, int alpha, int beta, bool isMaximizing)
        {
            int currentEval = evaluator.Execute(_position);
            if (depth == 0)
            {
                return new MoveTree(new Node() { Eval = currentEval, OriginalEval = currentEval });
            }

            List<Move> possibleMoves = GenerateMoves(_position);
            if (possibleMoves.Count == 0)
            {
                return new MoveTree(new Node() { Eval = currentEval, OriginalEval = currentEval });
            }

            Node rootNode = new Node() { Eval = isMaximizing ? int.MinValue : int.MaxValue, OriginalEval = currentEval };
            MoveTree? bestMoveTree = null;
            Move? bestMove = null;

            foreach (var move in possibleMoves)
            {
                TotalMoves++;

                move.Execute(_position);
                MoveTree childTree = EvaluateBestContinuation(_position, depth - 1, alpha, beta, !isMaximizing);
                int afterMoveEval = evaluator.Execute(_position);
                _position.Undo();

                int childEval = childTree.Root.Eval;

                if (isMaximizing)
                {
                    if (childEval > rootNode.Eval)
                    {
                        rootNode.OriginalEval = afterMoveEval;
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
                        rootNode.OriginalEval = afterMoveEval;
                        rootNode.Eval = childEval;
                        bestMoveTree = childTree;
                        bestMove = move;
                    }
                    beta = Math.Min(beta, childEval);
                }

                if (beta <= alpha) // Alpha-beta pruning
                {
                    break;
                }
            }

            if (bestMoveTree == null)
            {
                bestMoveTree = new MoveTree(new Node() { Eval = rootNode.Eval });
            }

            rootNode.Value = bestMove;
            rootNode.Subnodes.Add(bestMoveTree.Root);
            return new MoveTree(rootNode);
        }

        public override List<MoveTree> GetInitialMoveTrees(Board _position, int depth)
        {
            List<MoveTree> moveTrees = new List<MoveTree>();
            List<Move> possibleMoves = GenerateMoves(_position);
            if (possibleMoves.Count == 0) return moveTrees;

            bool isMaximizing = _position.SidePlayable == PlayerSide.White;
            int alpha = int.MinValue, beta = int.MaxValue;

            foreach (var move in possibleMoves)
            {
                move.Execute(_position);
                MoveTree bestContinuation = EvaluateBestContinuation(_position, depth - 1, alpha, beta, !isMaximizing);
                _position.Undo();

                Node rootNode = new Node() { Value = move, Eval = bestContinuation.Root.Eval, OriginalEval = evaluator.Execute(_position) };
                rootNode.Subnodes.Add(bestContinuation.Root);

                moveTrees.Add(new MoveTree(rootNode));

                // Update alpha-beta bounds
                if (isMaximizing)
                    alpha = Math.Max(alpha, rootNode.Eval);
                else
                    beta = Math.Min(beta, rootNode.Eval);

                if (beta <= alpha) // Alpha-beta pruning
                    break;
            }

            return moveTrees;
        }

    }
}
