using Knightmare.Moves;

namespace Knightmare.Views
{
    internal class MoveTreeView
    {
        Queue<Node> queue;
        MoveView moveView;

        public MoveTreeView()
        {
            queue = new Queue<Node>();
            moveView = new MoveView();
        }

        public void Print(MoveTree _move)
        {
            int column = 0;
            string cell = "";

            foreach (Node node in _move.Root().Children())
            {
                cell += $"{moveView.MoveToString(node.Value())} Eval: {node.eval} | ";

                column++;
                if (column >= 8)
                {
                    Console.WriteLine(cell);
                    cell = "";
                    column = 0;
                }
            }

            if (!string.IsNullOrEmpty(cell))
            {
                Console.WriteLine(cell);
            }
        }
    }
}
