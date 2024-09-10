using Knightmare.Moves;
using Knightmare.Boards;
using Knightmare.Pieces;

namespace Knightmare.Algorithm
{
    internal class DFS : IGraphSearch
    {
        public int TotalMoves { get; set; }
        public long ElapsedTime { get; set; }
        private readonly IEvaluation evaluator;

        public DFS(IEvaluation _evaluator)
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
            SimpleEvaluation simpleEvaluation = new SimpleEvaluation();

            foreach (ChessPiece piece in pieces)
            {
                copy = _position.Copy();
                moveRange = piece.MoveRange(copy);

                foreach (Move move in moveRange)
                {
                    copy = _position.Copy();
                    newPosition = copy.Update(move);
                    node = new Node(move, newPosition);
                    node.eval = simpleEvaluation.Execute(newPosition);
                    TotalMoves++;

                    treeRoot = new MoveTree(node);

                    movetree = moveTree(treeRoot, newPosition, enemy, me, level, treeRoot.Root());
                    movetrees[movetree.Root().eval] = movetree;
                }
            }

            return movetrees;
        }

        private MoveTree moveTree(MoveTree _movetree, Board _position, Robot me, Robot enemy, int level, Node _root)
        {
            if (level == 0)
            {
                return _movetree;
            }

            List<ChessPiece> pieces = _position.SidePieces(me.Side()).Values.ToList();
            Board newPosition = _position;
            Node node = new Node();
            SimpleEvaluation simpleEvaluation = new SimpleEvaluation();
            Board copy = new Board();
            List<Move> moveRange = new List<Move>();

            foreach (ChessPiece piece in pieces)
            {
                copy = _position.Copy();
                moveRange = piece.MoveRange(copy);

                foreach (Move move in moveRange)
                {
                    copy = _position.Copy();
                    newPosition = copy.Update(move);
                    node = new Node(move, newPosition, _root);
                    TotalMoves++;

                    node.eval = simpleEvaluation.Execute(newPosition);

                    _movetree = _movetree.Insert(node, _root);
                    _movetree = moveTree(_movetree, newPosition, enemy, me, level - 1, node);
                }
            }

            return _movetree;
        }
    }
}
