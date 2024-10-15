namespace Knightmare.Moves
{
    internal sealed class MoveTree
    {
        public Node root { get; set; }

        public MoveTree(Node _root = null)
        {
            root = _root;
        }

        public Node Root()
        {
            return root;
        }

        public MoveTree Insert(Node _node, Node _root = null)
        {
            if (root == null)
            {
                return new MoveTree(_node);
            }

            InsertIntoNode(_node, _root);
            return this;
        }

        private void InsertIntoNode(Node _node, Node _root)
        {
            _root.AddChild(_node);
        }
    }
}
