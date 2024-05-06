public class Node
{
    private readonly Move value;
    private readonly Node parent;
    private readonly List<Node> children;

    public Node(Move _move, List<Node> _children = null, Node _node = null)
    {
        value = _move;
        children = _children;
        parent = _node;
    }

    public Move Value()
    {
        return value;
    }

    public Node Value(Move _move)
    {
        return new Node(_move, children);
    }

    public Node Parent()
    {
        return parent;
    }

    public List<Node> Children()
    {
        return children;
    }

    public Node Child(int idx)
    {
        return children.ElementAt(idx);
    }
}
