public class Node
{
    private readonly Node left;
    private readonly Node right;
    private readonly Move value;

    public Node(Move _move, Node _left = null, Node _right= null)
    {
        left = _left;
        right = _right;
        value = _move;
    }

    public Move Value()
    {
        return value;
    }

    public Node Left()
    {
        return left;
    }

    public Node Right()
    {
        return right;
    }

    public Node Value(Move _move)
    {
        return new Node(_move, left, right);
    }

    public Node Left(Node _node)
    {
        return new Node(value, _node, right);
    }

    public Node Right(Node _node)
    {
        return new Node(value, left, _node);
    }
}
