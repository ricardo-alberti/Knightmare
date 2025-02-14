using Knightmare.Moves;

namespace Knightmare.Views
{
    internal class MoveTreeView
    {
        MoveView moveView;

        public MoveTreeView()
        {
            moveView = new MoveView();
        }

        public void PrintAllMoveTrees(List<MoveTree> moveTrees)
        {
            foreach (MoveTree tree in moveTrees)
            {
                PrintBestContinuation(tree.Root, 0);
                Console.WriteLine();
            }
        }

        private void PrintBestContinuation(Node _node, int depth)
        {
            if (_node.Value != null)
            {
                Console.WriteLine($"{new string(' ', depth * 4)}{_node?.Value?.Tiles()?[0]?.TilePiece?.Shape} {moveView.MoveToString(_node.Value)} E: {_node.Eval} OE: {_node.OriginalEval}");
            }

            Node? bestNode = _node.Subnodes.FirstOrDefault();
            if (bestNode != null)
            {
                PrintBestContinuation(bestNode, depth + 1);
            }
        }
    }
}
