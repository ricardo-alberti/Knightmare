public sealed class MoveTree
{
    private readonly Node root;

    public MoveTree(Node _root = null)
    {
        root = _root;
    }

    public Node Root()
    {
        return root;
    }

    public MoveTree Insert(Node _move, Node _node = null)
    {
        if (root == null)
        {
            return new MoveTree(_move);
        }

        return InsertIntoNode(_move, _node, root);
    }

    private MoveTree InsertIntoNode(Node _move, Node _node, Node _root)
    {
        if (_root == _node) {
            root.Children().Add(_move);
            return new MoveTree(root);
        }

        return this;
    }

    public int Score()
    {
        return 0;
    }
}
