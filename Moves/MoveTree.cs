namespace Knightmare.Moves
{
    internal sealed class MoveTree
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

        public MoveTree Insert(Node _node, Node _root = null)
        {
            if (root == null)
            {
                return new MoveTree(_node);
            }

            if (_node.Value().Piece().Side() == _root.Value().Piece().Side())
            {
                throw new Exception("Same PlayerSide adding on root node in movetree");
            }

            InsertIntoNode(_node, _root);
            return this;
        }

        private void InsertIntoNode(Node _node, Node _root)
        {
            Node bestChild = _root.bestChild;
            Node child = _node;

            _root.AddChild(_node);

            if (bestChild == null)
            {
                SetBestContinuation(_node, _root);
                return;
            }

            if (_node.Value().Piece().Side() == PlayerSide.White
                && bestChild.eval < _node.eval)
            {
                SetBestContinuation(_node, _root);
            }

            else if (_node.Value().Piece().Side() == PlayerSide.Black
                && bestChild.eval > _node.eval)
            {
                SetBestContinuation(_node, _root);
            }
        }

        public void SetBestContinuation(Node _continuation, Node _root)
        {
            Node curr = _root;
            while (curr != null)
            {
                curr.bestChild = bestChildNode(curr);
                curr.eval = curr.bestChild.eval;

                curr = curr.Parent();
            }
        }

        public Node bestChildNode(Node _root)
        {
            List<Node> children = _root.Children();
            Node bestNode = _root.Children().First();
            PlayerSide side = bestNode.Value().Piece().Side();

            foreach (Node child in children)
            {
                if (side == PlayerSide.White && bestNode.eval < child.eval)
                {
                    bestNode = child;
                }

                else if (side == PlayerSide.Black && bestNode.eval > child.eval)
                {
                    bestNode = child;
                }
            }

            return bestNode;
        }
    }
}
