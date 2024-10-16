using Knightmare.Moves;
using Knightmare.Boards;
using Knightmare.Pieces;
using System.Diagnostics;

namespace Knightmare.Algorithm
{
    internal class Minimax : ITreeSearch
    {
        public int TotalMoves { get; set; }
        public long ElapsedTime { get; set; }
        private readonly IEvaluation evaluator;

        public Minimax() : this(new SimpleEvaluation()) { }
        public Minimax(IEvaluation _evaluator)
        {
            TotalMoves = 0;
            ElapsedTime = 0;
            evaluator = _evaluator;
        }

        public MoveTree Execute(Board _position, Robot me, Robot enemy, int level)
        {
            var watch = Stopwatch.StartNew();

            bool isMaximizing = me.Side() == PlayerSide.White;
            var bestMoveTree = Evaluate(_position, me, enemy, level, isMaximizing);

            watch.Stop();
            ElapsedTime = watch.ElapsedMilliseconds;

            return bestMoveTree;
        }

        private MoveTree Evaluate(Board _position, Robot me, Robot enemy, int depth, bool isMaximizing)
        {
            // or game over adicionar dps
            if (depth == 0)
            {
                int eval = evaluator.Execute(_position);
                return new MoveTree(new Node(null, _position) { eval = eval });
            }

            List<Move> possibleMoves = GenerateMoves(_position, me);
            if (possibleMoves.Count == 0)
            {
                int eval = evaluator.Execute(_position);
                return new MoveTree(new Node(null, _position) { eval = eval });
            }

            MoveTree bestMoveTree = null;

            foreach (var move in possibleMoves)
            {
                TotalMoves++;
                Board newPosition = _position.Copy().Update(move);

                MoveTree childTree = Evaluate(newPosition, enemy, me, depth - 1, !isMaximizing);

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

        private List<Move> GenerateMoves(Board _position, Robot robot)
        {
            List<Move> allMoves = new List<Move>();
            List<ChessPiece> pieces = _position.SidePieces(robot.Side()).Values.ToList();


            foreach (ChessPiece piece in pieces)
            {
                allMoves.AddRange(piece.MoveRange(_position.Copy()));
            }

            return allMoves;
        }
    }
}
