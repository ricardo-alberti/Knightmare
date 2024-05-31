public class Node
{
    private readonly Move value;
    private readonly List<Node> children;

    public Node() : this (new Move(), new List<Node>()) { }

    public Node(Move _move) : this (_move, new List<Node>()) { }

    public Node(Move _move, List<Node> _children)
    {
        value = _move;
        children = _children;
    }

    public Move Value()
    {
        return value;
    }

    public Node Value(Move _move)
    {
        return new Node(_move, children);
    }

    public List<Node> Children()
    {
        return children;
    }

    public Node Child(int idx)
    {
        if (Children().Count > 0) 
        {
            return new Node();
        }

        return children.ElementAt(idx);
    }
}
