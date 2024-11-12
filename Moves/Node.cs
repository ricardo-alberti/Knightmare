using Knightmare.Boards;

namespace Knightmare.Moves
{
    internal class Node
    {
        private readonly List<Node> children;
        private readonly Board position;
        public Move value { get; set; }
        public int eval { get; set; }

        public Node() : this(new Move(), new List<Node>(), new Board()) { }
        public Node(Move _move) : this(_move, new List<Node>(), new Board()) { }
        public Node(Move _move, Board _position) : this(_move, new List<Node>(), _position) { }

        public Node(Move _move, List<Node> _children, Board _position)
        {
            value = _move;
            eval = 0;
            children = _children;
            position = _position;
        }

        public Board Position()
        {
            return position;
        }

        public Move Value()
        {
            return value;
        }

        public Node Value(Move _move)
        {
            return new Node(_move, children, position);
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
