public sealed class MoveTree
{
    public Node root { get; }

    public MoveTree(Node _root = null)
    {
        root = _root;
    }

    public Node Root()
    {
        return root;
    }

    public MoveTree Insert(Move _move)
    {
        if (root == null)
        {
            return new MoveTree(new Node(_move));
        }

        return new MoveTree(InsertRecursively(root, _move));
    }

    private Node InsertRecursively(Node _root, Move _move)
    {
        if (_root == null)
        {
            return new Node(_move);
        }

        if (_root.Left() == null)
        {
            return new Node(_root.Value(), InsertRecursively(_root.Left(), _move), _root.Right());
        }

        if (_root.Right() == null)
        {
            return new Node(_root.Value(), _root.Left(), InsertRecursively(_root.Right(), _move));
        }
        
        return new Node(_root.Value(), _root.Left(), InsertRecursively(_root.Right(), _move));
    }
}
