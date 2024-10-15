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
            int level = 0;
            PrintBestMoves(_move.Root(), level);
        }

        private void PrintBestMoves(Node _node, int _level)
        {

        }
    }
}
