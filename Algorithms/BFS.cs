using Knightmare.Moves;
using Knightmare.Boards;
using Knightmare.Pieces;

namespace Knightmare.Algorithm
{
    internal class BFS : IGraphSearch
    {
        public int TotalMoves { get; set; }
        public long ElapsedTime { get; set; }
        private readonly IEvaluation evaluator;

        public BFS(IEvaluation _evaluator)
        {
            TotalMoves = 0;
            ElapsedTime = 0;
            evaluator = _evaluator;
        }

        public MoveTree Execute(Board _position, Robot me, Robot enemy, int level)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            Dictionary<int, MoveTree> trees = moveTrees(_position, me, enemy, new Dictionary<int, MoveTree>(), level);
            MoveTree movetree = me.Side() == PlayerSide.White ? trees[trees.Keys.Max()] : trees[trees.Keys.Min()];

            watch.Stop();
            ElapsedTime = watch.ElapsedMilliseconds;

            return movetree;
        }

        private Dictionary<int, MoveTree> moveTrees(Board _position, Robot me, Robot enemy, Dictionary<int, MoveTree> movetrees, int level)
        {
            List<ChessPiece> pieces = _position.SidePieces(me.Side()).Values.ToList();
            MoveTree movetree = new MoveTree();
            Board copy = new Board();
            List<Move> moveRange = new List<Move>();
            Board newPosition = new Board();
            Node node = new Node();
            MoveTree treeRoot = new MoveTree();

            foreach (ChessPiece piece in pieces)
            {
                copy = _position.Copy();
                moveRange = piece.MoveRange(copy);

                foreach (Move move in moveRange)
                {
                    copy = _position.Copy();
                    newPosition = copy.Update(move);
                    node = new Node(move, newPosition, null);
                    node.eval = evaluator.Execute(newPosition);
                    treeRoot = new MoveTree(node);

                    TotalMoves++;

                    movetree = moveTree(treeRoot, newPosition, enemy, me, level, treeRoot.Root());
                    movetrees[movetree.Root().eval] = movetree;
                }
            }

            return movetrees;
        }

        private MoveTree moveTree(MoveTree _movetree, Board _position, Robot me, Robot enemy, int level, Node _root)
        {
            Queue<(Node, Board, Robot, int)> queue = new Queue<(Node, Board, Robot, int)>();
            queue.Enqueue((_root, _position, me, level));

            while (queue.Count > 0)
            {
                var (currentNode, currentPosition, currentRobot, currentLevel) = queue.Dequeue();

                if (currentLevel == 0)
                {
                    continue;
                }

                List<ChessPiece> pieces = currentPosition.SidePieces(currentRobot.Side()).Values.ToList();
                foreach (ChessPiece piece in pieces)
                {
                    List<Move> moveRange = piece.MoveRange(currentPosition);

                    foreach (Move move in moveRange)
                    {
                        Board newPosition = currentPosition.Copy().Update(move);

                        Node newNode = new Node(move, newPosition, currentNode);
                        newNode.eval = evaluator.Execute(newPosition);

                        TotalMoves++;

                        _movetree = _movetree.Insert(newNode, currentNode);

                        queue.Enqueue((newNode, newPosition, enemy, currentLevel - 1));
                    }
                }
            }

            return _movetree;
        }
    }
}
