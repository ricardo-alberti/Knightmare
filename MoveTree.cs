public sealed class MoveTree
{
    private Node root;

    public MoveTree(Node _root = null)
    {
        root = _root;
    }

    public Node Root()
    {
        return root;
    }

    public Node Insert(Move _move, Node _node = null)
    {
        if (root == null)
        {
            return new Node(_move);
        }

        return InsertIntoNode(_move, _node);
    }

    public Node InsertIntoNode(Move _move, Node _node)
    {
        Node node = new Node(_move);

        if (_node == null)
        {
            return null;
        }

        foreach (Node child in _node.Children())
        {
            Node result = InsertIntoNode(_move, child);

            if (result != null)
            {
                return result;
            }
        }

        return null;
    }

    public int Score()
    {
        return 0;
    }
}
