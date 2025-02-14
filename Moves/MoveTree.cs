namespace Knightmare.Moves
{
    internal sealed class MoveTree
    {
        public Node Root { get; set; }

        public MoveTree(Node _root)
        {
            Root = _root;
        }
    }
}
