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

        return new MoveTree(InsertIntoNode(_move, _node, root));
    }

    private Node InsertIntoNode(Node _move, Node _node, Node _root)
    {
        if (_root == _node)
        {
            _root.Children().Add(_move);
            _root.Value().UpdateEval(average(_root));
            return root;
        }

        foreach (var child in _root.Children())
        {
            var result = InsertIntoNode(_move, _node, child);
            if (result != null)
            {
                child.Children().Add(_move);
                child.Value().UpdateEval(child.Value().Eval());
                return root;
            }
        }

        return null;
    }

    private int average(Node _root)
    {
        if (_root.Children().Count == 0)
        {
            return root.Value().Eval();
        }

        int sum = 0;
        foreach (Node child in _root.Children())
        {
            sum += average(child);
        }

        return _root.Value().Eval() + (sum / _root.Children().Count);
    }

    public void Print(Node _root)
    {
        Console.Write("\n1. ");
        _root.Value().Print();

        int i = 2;
        if (_root.Children().Count > 0)
        {
            Console.Write("\n");
            Console.Write(" | ");
            Console.Write(i);
            Console.Write(". ");

            _root.Children().First().Value().Print("\t");
            i++;

            Console.Write("\n");
            if (_root.Children().First().Children().Count > 0)
            {
                Print(_root.Children().First().Children().First());
            }
        }
    }
}
