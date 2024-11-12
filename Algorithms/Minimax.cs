using Knightmare.Moves;
using Knightmare.Boards;
using System.Diagnostics;

namespace Knightmare.Algorithm
{
    internal class Minimax : TreeSearch
    {
        public Minimax() : this(new SimpleEvaluation()) { }
        public Minimax(IEvaluation _evaluator) : base(_evaluator)
        {

        }

        public override MoveTree BestTree(Board _position, Robot me, Robot enemy, int level)
        {
            var watch = Stopwatch.StartNew();

            bool isMaximizing = me.Side() == PlayerSide.White;
            var bestMoveTree = EvaluateTrees(_position, me, enemy, level, isMaximizing);

            watch.Stop();
            ElapsedTime = watch.ElapsedMilliseconds;

            return bestMoveTree;
        }

        protected MoveTree EvaluateTrees(Board _position, Robot me, Robot enemy, int depth, bool isMaximizing)
        {
            // or game over adicionar dps
            if (depth == 0)
            {
                int eval = evaluator.Execute(_position);
                return new MoveTree(new Node() { eval = eval });
            }

            List<Move> possibleMoves = this.GenerateMoves(_position, me);
            if (possibleMoves.Count == 0)
            {
                int eval = evaluator.Execute(_position);
                return new MoveTree(new Node() { eval = eval });
            }

            MoveTree? bestMoveTree = null;

            foreach (var move in possibleMoves)
            {
                TotalMoves++;
                Board newPosition = _position.Copy().Update(move);

                MoveTree childTree = EvaluateTrees(newPosition, enemy, me, depth - 1, !isMaximizing);

                if (bestMoveTree == null)
                {
                    bestMoveTree = new MoveTree(new Node(move, newPosition) { eval = childTree.Root().eval });
                }

                if (isMaximizing)
                {
                    if (childTree.Root().eval > (bestMoveTree.Root().eval))
                    {
                        bestMoveTree.Root().value = move;
                        bestMoveTree.Root().eval = childTree.Root().eval;
                    }
                }
                else
                {
                    if (childTree.Root().eval < (bestMoveTree.Root().eval))
                    {
                        bestMoveTree.Root().value = move;
                        bestMoveTree.Root().eval = childTree.Root().eval;
                    }
                }
            }

            return bestMoveTree;
        }
    }
}