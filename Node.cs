using System.Data;

public class Node
{
    private readonly Move value;
    private readonly int eval;
    private readonly List<Node> children;

    public Node() : this (new Move(), new List<Node>(), 0) { }

    public Node(Move _move) : this (_move, new List<Node>(), 0) { }

    public Node(Move _move, int _eval) : this (_move, new List<Node>(), _eval) { }

    public Node(Move _move, List<Node> _children, int _eval)
    {
        value = _move;
        eval = _eval;
        children = _children;
    }

    public Move Value()
    {
        return value;
    }

    public Node Value(Move _move)
    {
        return new Node(_move, children, eval);
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
