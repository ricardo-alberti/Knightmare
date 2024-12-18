using Knightmare.Boards;

namespace Knightmare.Moves
{
    internal class Node
    {
        private readonly List<Node> children;
        public Move value { get; set; }
        public int eval { get; set; }

        public Node() : this(new Move(), new List<Node>()) { }
        public Node(Move _move) : this(_move, new List<Node>()) { }

        public Node(Move _move, List<Node> _children)
        {
            value = _move;
            eval = 0;
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

        public void AddChild(Node _child)
        {
            children.Add(_child);
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
}
